using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BazaarNotifier.Lib
{
    public class BazaarAnalyzer
    {
        const int HoursInWeek = 168;
        public static List<FlipAnalyzedBazaarItem> AnalyzeFlips(List<BazaarItem> items, double budget)
        {
            List<FlipAnalyzedBazaarItem> analyzedItems = new List<FlipAnalyzedBazaarItem>(items.Count);
            foreach(var item in items)
            {
                double margin = item.BuyPrice - item.SellPrice;
                long itemsUserCanAfford = (long)(budget / item.SellPrice);
                long limitingVolumeMetric = Math.Min(item.BuyMovingWeek, item.SellMovingWeek);
                double maxItemsPerHour = limitingVolumeMetric / (double) HoursInWeek;
                double maxHourlyProfit = 0;
                bool isLimitedByBudget;
                if(maxItemsPerHour > itemsUserCanAfford)
                {
                    isLimitedByBudget = true;
                    maxHourlyProfit = margin * itemsUserCanAfford;
                }
                else
                {
                    isLimitedByBudget = false;
                    maxHourlyProfit = margin * maxItemsPerHour;
                }
                maxHourlyProfit = Math.Max(maxHourlyProfit, 0);
                analyzedItems.Add(FlipAnalyzedBazaarItem.FromResult(item, maxHourlyProfit, isLimitedByBudget));
            }
            return analyzedItems.OrderByDescending(item => item.PotentialHourlyProfit).ToList();
        }
    }
}
