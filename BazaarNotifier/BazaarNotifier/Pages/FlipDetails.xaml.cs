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

        private string SellPriceFormatted
        {
            get
            {
                if (Item != null)
                {
                    return Item.SellPrice.ToString("n2");
                }
                else
                {
                    return "";
                }
            }
        }
        private string BuyPriceFormatted
        {
            get
            {
                if (Item != null)
                {
                    return Item.BuyPrice.ToString("n2");
                }
                else
                {
                    return "";
                }
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
            Item = BazaarAnalyzer.AnalyzeItem(bazaarItem, true);
            OnPropertyChanged("Item");
            OnPropertyChanged("SellPriceFormatted");
            OnPropertyChanged("BuyPriceFormatted");
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
