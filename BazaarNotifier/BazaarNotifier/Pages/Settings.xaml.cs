using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BazaarNotifier.Lib;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BazaarNotifier.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {

        public bool ShowStatusBar { get; set; } = BazaarAppContext.Settings.ShowStatusBar;
        public double Budget { get; set; } = BazaarAppContext.Settings.Budget;
        public bool AutoRefreshEnabled { get; set; } = BazaarAppContext.Settings.AutoRefreshEnabled;
        public int AutoRefreshLimit { get; set; } = BazaarAppContext.Settings.AutoRefreshLimit;
        public long MinimumVolume { get; set; } = BazaarAppContext.Settings.MinimumVolume;
        public double MaxPriceRatio { get; set; } = BazaarAppContext.Settings.MaxPriceRatio;
        public Settings()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            BazaarAppContext.Settings.ShowStatusBar = ShowStatusBar;
            BazaarAppContext.Settings.Budget = Budget;
            //BazaarAppContext.Settings.AutoRefreshEnabled = AutoRefreshEnabled;
            //BazaarAppContext.Settings.AutoRefreshLimit = AutoRefreshLimit;
            BazaarAppContext.Settings.MinimumVolume = MinimumVolume;
            BazaarAppContext.Settings.MaxPriceRatio = MaxPriceRatio;
            AppData.Save("settings", BazaarAppContext.Settings);
            ContentDialog dialog = new ContentDialog
            {
                Title = "Saved",
                Content = "Your settings have been saved",
                CloseButtonText = "Ok",
                XamlRoot = XamlRoot
            };
            dialog.ShowAsync();
            BazaarAppContext.SettingsWereUpdated();
        }
    }
}
