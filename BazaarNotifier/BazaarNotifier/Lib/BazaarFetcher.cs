using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib
{
    public class BazaarFetcher
    {
        public PeriodicTimer Timer { get; set; }
        public bool Running { get; set; } = true;
        public event EventHandler<BazaarFetchedEventArgs> Fetched;
        protected HypixelAPI API { get; set; } = new();
        public List<BazaarItem> LastFetch { get; set; } = null;
        public async Task Tick()
        {
            while(await Timer.WaitForNextTickAsync() && Running)
            {
                await Fetch();
            }
        }
        public async Task Fetch()
        {
            var bazaar = await API.GetBazaar();
            foreach (var item in bazaar)
            {
                item.Name = BazaarAppContext.Items.Where(i => i.ID == item.ID).FirstOrDefault()?.Name
                                                        ?? FormatUnknownItemName(item.ID);
            }
            BazaarAppContext.DispatcherQueue.TryEnqueue(() =>
            {
                LastFetch = bazaar;
                Fetched.Invoke(this, new BazaarFetchedEventArgs(bazaar));
            });
        }
        public async void Start()
        {
            // TODO: un-hardcode this
            Timer = new PeriodicTimer(TimeSpan.FromMilliseconds(BazaarAppContext.Settings.AutoRefreshDelay));
            await Fetch();
            await Tick();
        }
        private string FormatUnknownItemName(string itemId)
        {
            return new CultureInfo("en-US", false)
                    .TextInfo.ToTitleCase(itemId.ToLower().Replace("_", " ").Replace(":", ""));
        }
    }
}
