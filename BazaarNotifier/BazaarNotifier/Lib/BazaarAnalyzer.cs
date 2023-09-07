using BazaarNotifier.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BazaarNotifier.Lib
{
    public class BazaarAnalyzer
    {
        const int HoursInWeek = 168;
        public static List<FlipAnalyzedBazaarItem> AnalyzeFlips(List<BazaarItem> items)
        {
            List<FlipAnalyzedBazaarItem> analyzedItems = new List<FlipAnalyzedBazaarItem>(items.Count);
            foreach(var item in items)
            {
                // Function will return null if filter rules are not met
                var analyzedItem = AnalyzeItem(item);
                if(analyzedItem != null)
                    analyzedItems.Add(analyzedItem);
            }
            return analyzedItems.OrderByDescending(item => item.PotentialHourlyProfit).ToList();
        }
        public static FlipAnalyzedBazaarItem AnalyzeItem(BazaarItem item, bool ignoreFilters=false)
        {
            // some filtering rules - return null if violated
            double maxPriceRatio = BazaarAppContext.Settings.MaxPriceRatio;
            long minimumVolume = BazaarAppContext.Settings.MinimumVolume;
            // The amount of money the player has available
            double budget = BazaarAppContext.Settings.Budget;

            // Margin is the amount of coins you might be able to make
            double margin = item.BuyPrice - item.SellPrice;
            // Price ratio describes the margin as a percentage
            double priceRatio = item.BuyPrice / item.SellPrice;
            // The count of items the player can afford
            long itemsUserCanAfford = (long)(budget / item.SellPrice);
            // The monimum items which can be moved - either bought or sold
            long limitingVolumeMetric = Math.Min(item.BuyMovingWeek, item.SellMovingWeek);

            // How many items can be moved per hour?
            double maxItemsPerHour = limitingVolumeMetric / (double)HoursInWeek;
            // The important metric - how much $$$ can you theoretically make per hr?
            double maxHourlyProfit = 0;
            // Whether or not having a larger budget will increase profits
            bool isLimitedByBudget;
            if (maxItemsPerHour > itemsUserCanAfford)
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

            var result = FlipAnalyzedBazaarItem.FromResult(item, maxHourlyProfit, isLimitedByBudget);

            // Skip rule checking if filters are turned off (pure analysis mode)
            if (ignoreFilters)
            {
                return result;
            }
            
            if (limitingVolumeMetric >= minimumVolume &&
                    (maxPriceRatio < 0.1 || priceRatio <= maxPriceRatio))
            {
                
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
