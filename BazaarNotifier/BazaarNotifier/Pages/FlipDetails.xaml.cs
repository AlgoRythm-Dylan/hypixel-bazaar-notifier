using BazaarNotifier.Lib;
using BazaarNotifier.Lib.Models;
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
    public sealed partial class FlipDetails : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private FlipAnalyzedBazaarItem Item { get; set; }

        private double Budget
        {
            get
            {
                return BazaarAppContext.Settings.Budget;
            }
        }

        private long AmountCanAfford
        {
            get
            {
                if (Item != null)
                    return (long)(BazaarAppContext.Settings.Budget / Item.TopSellOrderPrice);
                else
                    return 0;
            }
        }

        private long CanBuyPerOneMinute
        {
            get
            {
                if (Item != null)
                    return Item.SellMovingWeek / 168 / 60; // 168 hours in a week
                else
                    return 0;
            }
        }

        private long CanSellPerOneMinute
        {
            get
            {
                if (Item != null)
                    return Item.BuyMovingWeek / 168 / 60;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Details page for a bazaar flip item. Right now,
        /// this page can only be accessed if the bazaar has
        /// fetcher has already loaded
        /// </summary>
        public FlipDetails()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var id = e.Parameter as string;
            var bazaarItem = BazaarAppContext.BazaarFetcher
                                             .LastFetch
                                             .FirstOrDefault(i => i.ID == id);
            // Analyze item, but don't use global filters
            OnSetItem(bazaarItem);
            BazaarAppContext.BazaarFetcher.Fetched += BazaarFetcher_Fetched;
        }

        private void BazaarFetcher_Fetched(object sender, BazaarFetchedEventArgs e)
        {
            var bazaarItem = BazaarAppContext.BazaarFetcher
                                             .LastFetch
                                             .FirstOrDefault(i => i.ID == Item.ID);
            OnSetItem(bazaarItem);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            BazaarAppContext.BazaarFetcher.Fetched -= BazaarFetcher_Fetched;
        }

        public void OnSetItem(BazaarItem item)
        {
            Item = BazaarAnalyzer.AnalyzeItem(item, true);
            OnPropertyChanged("Item");
            OnPropertyChanged("AmountCanAfford");
            OnPropertyChanged("CanBuyPerOneMinute");
            OnPropertyChanged("CanSellPerOneMinute");
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
