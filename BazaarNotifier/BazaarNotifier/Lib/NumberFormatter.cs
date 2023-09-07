using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaarNotifier.Lib
{
    public static class NumberFormatter
    {
        private const long billion = 1_000_000_000;
        private const long million = 1_000_000;
        private const long thousand = 1_000;

        public static string FormatNumber(long number)
        {
            if(number >= billion)
            {
                return $"{Math.Round((double)number / billion)}B";
            }
            else if (number >= million)
            {
                return $"{Math.Round((double)number / million)}M";
            }
            else if (number >= thousand)
            {
                return $"{Math.Round((double)number / thousand)}K";
            }
            return number.ToString();
        }
    }
}
