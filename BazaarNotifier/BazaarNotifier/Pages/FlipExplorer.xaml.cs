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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace BazaarNotifier.Pages
{
    public sealed partial class FlipExplorer : Page
    {
        public List<FlipAnalyzedBazaarItem> AnalyzedItems { get; set; } = new();
        public FlipExplorer()
        {
            InitializeComponent();
            BazaarAppContext.BazaarFetcher.Fetched += BazaarFetcher_Fetched;
            if(BazaarAppContext.BazaarFetcher.LastFetch != null)
            {
                DisplayBazaarItems(BazaarAppContext.BazaarFetcher.LastFetch);
            }
        }

        private void BazaarFetcher_Fetched(object sender, BazaarFetchedEventArgs e)
        {
            DisplayBazaarItems(e.Items);
        }

        private void DisplayBazaarItems(List<BazaarItem> items)
        {
            LoadingSpinner.Visibility = Visibility.Collapsed;
            AnalyzedItems = BazaarAnalyzer.AnalyzeFlips(items, BazaarAppContext.Settings.Budget)
                                                       .Take(25).ToList();
            ItemsGrid.ItemsSource = AnalyzedItems;
        }
    }
}
