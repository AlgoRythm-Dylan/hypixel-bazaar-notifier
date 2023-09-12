using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public int CurrentFetchCount { get; set; } = 0;
        public async Task Tick()
        {
            while(await Timer.WaitForNextTickAsync() && Running && ShouldTick())
            {
                await Fetch();
                CurrentFetchCount++;
            }
        }
        public async Task Fetch()
        {
            var bazaar = await API.GetBazaar();
            foreach (var item in bazaar)
            {
                var relatedItem = BazaarAppContext.Items.Where(i => i.ID == item.ID).FirstOrDefault();
                if (relatedItem != null)
                {
                    item.Name = StripColorCodes(relatedItem.Name);
                }
                else
                {
                    item.Name = FormatUnknownItemName(item.ID);
                }
            }
            BazaarAppContext.DispatcherQueue.TryEnqueue(() =>
            {
                LastFetch = bazaar;
                Fetched?.Invoke(this, new BazaarFetchedEventArgs(bazaar));
            });
        }
        protected bool ShouldTick()
        {
            return BazaarAppContext.Settings.AutoRefreshLimit == 0 ||
                   CurrentFetchCount < BazaarAppContext.Settings.AutoRefreshLimit;
        }
        public async void Start()
        {
            Timer = new PeriodicTimer(TimeSpan.FromMilliseconds(BazaarAppContext.Settings.AutoRefreshDelay));
            CurrentFetchCount = 0;
            Running = true;
            await Fetch();
            await Tick();
        }
        private string StripColorCodes(string itemId)
        {
            return Regex.Replace(itemId, "§.", "");
        }
        private string FormatUnknownItemName(string itemId)
        {
            var formatted = itemId.ToLower().Replace("_", " ").Replace(":", "");
            formatted = StripColorCodes(formatted);
            return new CultureInfo("en-US", false).TextInfo.ToTitleCase(formatted);
        }
    }
}
