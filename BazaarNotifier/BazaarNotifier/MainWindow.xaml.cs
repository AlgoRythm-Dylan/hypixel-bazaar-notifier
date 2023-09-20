using BazaarNotifier.Lib;
using BazaarNotifier.Lib.Models;
using BazaarNotifier.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace BazaarNotifier
{
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string CurrentNavLocation { get; set; } = null;
        private HypixelAPI API { get; set; } = new();
        private PeriodicTimer Ticker { get; set; }
        private bool IsClosed { get; set; } = false;
        private Visibility StatusBarVisibility
        {
            get
            {
                if (BazaarAppContext.Settings == null)
                    return Visibility.Visible;
                return BazaarAppContext.Settings.ShowStatusBar ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public MainWindow()
        {
            InitializeComponent();

            Title = "Bazaar Notifier";
            MainNav.ItemInvoked += OnNavigation;
            MainNav.BackRequested += BackRequested;
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(CustomTitleBar);

            LoadData();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, WindowEventArgs args)
        {
            IsClosed = true;
        }

        private void BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            // Back button is implemented, but has some complications
            // which make it not quite worth it - mainly keeping
            // track of which menu item should be selected. This is
            // especially complicated when you consider navigation
            // within a frame
            if(MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
            MainNav.IsBackEnabled = MainFrame.CanGoBack;
        }

        public async Task LoadData()
        {
            var settings = await AppData.Load<AppSettings>("settings");
            if (settings == null)
            {
                settings = new();
                await AppData.Save("settings", settings);
            }
            var items = await API.GetSkyblockItems();
            BazaarAppContext.DispatcherQueue.TryEnqueue(() =>
            {
                BazaarAppContext.Settings = settings;
                BazaarAppContext.Items = items;
                BazaarAppContext.BazaarFetcher.Start();
                MainFrame.Navigate(typeof(FlipExplorer));
                BazaarAppContext.SettingsUpdated += SettingsUpdated;
                BazaarAppContext.SettingsWereUpdated();
                Tick();
            });
        }

        private async void Tick()
        {
            Ticker = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
            while(await Ticker.WaitForNextTickAsync())
            {
                BazaarAppContext.DispatcherQueue.TryEnqueue(() =>
                {
                    OnTick();
                });
            }
        }

        private void OnTick()
        {
            if(BazaarAppContext.Settings.ShowStatusBar && !IsClosed)
            {
                var timeSinceLastUpdate = (DateTime.Now.Ticks - BazaarAppContext.BazaarFetcher.LastFetchTime.Ticks) / TimeSpan.TicksPerMillisecond;
                var timeUntilNextUpdate = Math.Max(0, BazaarAppContext.Settings.AutoRefreshDelay - timeSinceLastUpdate);
                var progress = 100 - ((double)timeUntilNextUpdate / BazaarAppContext.Settings.AutoRefreshDelay * 100);
                NextTickProgress.Value = progress;
            }
        }

        private void SettingsUpdated(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(StatusBarVisibility));
        }

        private void OnNavigation(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string tag = args.InvokedItemContainer.Tag as string;
            if (!string.IsNullOrEmpty(tag) && tag != CurrentNavLocation)
            {
                switch (tag)
                {
                    case "Explorer":
                        MainFrame.Navigate(typeof(FlipExplorer));
                        break;
                    case "Alerts":
                        MainFrame.Navigate(typeof(Notifications));
                        break;
                    case "Flipping Tutorial":
                        MainFrame.Navigate(typeof(Flipping));
                        break;
                    case "Settings":
                        MainFrame.Navigate(typeof(Settings));
                        break;
                }
            }
            MainNav.IsBackEnabled = MainFrame.CanGoBack;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
