using BazaarNotifier.Lib;
using BazaarNotifier.Lib.Models;
using BazaarNotifier.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace BazaarNotifier.Pages
{
    public sealed partial class FlipExplorer : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public List<FlipAnalyzedBazaarItem> AnalyzedItems { get; set; } = new();
        public long MinimumVolume
        {
            get
            {
                return BazaarAppContext.Settings.MinimumVolume;
            }
            set
            {
                BazaarAppContext.Settings.MinimumVolume = value;
                AnalyzeItems();
                AppData.Save("settings", BazaarAppContext.Settings);
            }
        }
        public FlipExplorer()
        {
            InitializeComponent();
            BazaarAppContext.BazaarFetcher.Fetched += BazaarFetcher_Fetched;
            if(BazaarAppContext.BazaarFetcher.LastFetch != null)
            {
                DisplayBazaarItems(BazaarAppContext.BazaarFetcher.LastFetch);
            }
        }

        // Unsubscribe when navigating away
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            BazaarAppContext.BazaarFetcher.Fetched -= BazaarFetcher_Fetched;
        }

        private void BazaarFetcher_Fetched(object sender, BazaarFetchedEventArgs e)
        {
            DisplayBazaarItems(e.Items);
        }

        private void DisplayBazaarItems(List<BazaarItem> items)
        {
            LoadingSpinner.Visibility = Visibility.Collapsed;
            AnalyzeItems(items);
        }
        public void AnalyzeItems(List<BazaarItem> items = null)
        {
            if (items == null)
                items = BazaarAppContext.BazaarFetcher.LastFetch;
            AnalyzedItems = BazaarAnalyzer.AnalyzeFlips(items).Take(25).ToList();
            ItemsGrid.ItemsSource = AnalyzedItems;
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ItemsGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as FlipAnalyzedBazaarItem;
            Frame.Navigate(typeof(FlipDetails), item.ID);
        }
    }
}
