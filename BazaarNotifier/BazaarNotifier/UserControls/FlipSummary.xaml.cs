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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BazaarNotifier.UserControls
{
    public sealed partial class FlipSummary : UserControl, INotifyPropertyChanged
    {
        private FlipAnalyzedBazaarItem _item = new();
        public FlipAnalyzedBazaarItem FlipItem
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                OnPropertyChanged("FlipItem");
                OnPropertyChanged("FormattedHourlyProfit");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public string FormattedHourlyProfit
        { 
            get
            {
                return FlipItem?.PotentialHourlyProfit.ToString("n2");
            }
        }
        public FlipSummary()
        {
            InitializeComponent();
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
