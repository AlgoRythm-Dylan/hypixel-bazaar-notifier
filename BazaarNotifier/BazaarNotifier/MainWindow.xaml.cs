using BazaarNotifier.Lib;
using BazaarNotifier.Pages;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace BazaarNotifier
{
    public sealed partial class MainWindow : Window
    {
        private string CurrentNavLocation { get; set; } = null;
        private HypixelAPI HypixelAPI { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
            BazaarAppContext.MainWindow = this;

            MainFrame.Navigate(typeof(FlipExplorer));

            Title = "Bazaar Notifier";
            MainNav.ItemInvoked += OnNavigation;
            MainNav.BackRequested += BackRequested;
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(CustomTitleBar);

            LoadItems();
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

        public async Task LoadItems()
        {
            var items = await HypixelAPI.GetSkyblockItems();
            DispatcherQueue.TryEnqueue(() =>
            {
                BazaarAppContext.Items = items;
            });
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
                    case "Tutorial":
                        MainFrame.Navigate(typeof(Flipping));
                        break;
                    case "Settings":
                        MainFrame.Navigate(typeof(Settings));
                        break;
                }
            }
            MainNav.IsBackEnabled = MainFrame.CanGoBack;
        }
    }
}
