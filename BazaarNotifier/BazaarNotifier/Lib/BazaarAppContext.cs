﻿using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib
{
    // Items shared between pages, used as a singleton
    public static class BazaarAppContext
    {
        public static AppSettings Settings { get; set; }
        public static List<Item> Items { get; set; }
        public static MainWindow MainWindow { get; set; }
        public static BazaarFetcher BazaarFetcher { get; set; }
    }
}