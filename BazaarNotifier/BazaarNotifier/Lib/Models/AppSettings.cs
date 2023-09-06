using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib.Models
{
    public class AppSettings
    {
        /// <summary>
        /// Budget used to calculate best flip. Default is a modest
        /// 10,000
        /// </summary>
        public double Budget { get; set; } = 10000.0;
        /// <summary>
        /// If auto refresh is enabled or not
        /// </summary>
        public bool AutoRefreshEnabled { get; set; } = true;
        /// <summary>
        /// Amount in ms to wait until next refresh, automatic
        /// or otherwise. Default is 1 minute, the minimum
        /// </summary>
        public int AutoRefreshDelay { get; set; } = 60000;
        /// <summary>
        /// In case the user accidentally leaves their computer running
        /// limit the auto refresh count so as to not waste bandwidth
        /// </summary>
        public int AutoRefreshLimit { get; set; } = 720;
    }
}
