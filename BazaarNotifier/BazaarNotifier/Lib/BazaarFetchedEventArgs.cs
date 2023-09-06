using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib
{
    public class BazaarFetchedEventArgs : EventArgs
    {
        public BazaarFetchedEventArgs(List<BazaarItem> items)
        {
            Items = items;
        }

        public List<BazaarItem> Items { get; set; }
    }
}
