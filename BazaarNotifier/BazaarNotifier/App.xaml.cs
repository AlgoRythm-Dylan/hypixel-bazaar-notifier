using BazaarNotifier.Lib;
using BazaarNotifier.Lib.Models;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BazaarNotifier
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private HypixelAPI API { get; set; } = new();
        public App()
        {
            InitializeComponent();
            BazaarAppContext.DispatcherQueue = DispatcherQueue.GetForCurrentThread();
            LoadData();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        public async Task LoadData()
        {
            var settings = await AppData.Load<AppSettings>("settings");
            if(settings == null)
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
            });
        }

        private Window m_window;
    }
}
