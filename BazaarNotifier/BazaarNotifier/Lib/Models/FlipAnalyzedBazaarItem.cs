namespace BazaarNotifier.Lib.Models
{
    public class FlipAnalyzedBazaarItem : BazaarItem
    {
        public double PotentialHourlyProfit { get; set; }
        public bool IsLimitedByBudget { get; set; }
        public static FlipAnalyzedBazaarItem FromResult(BazaarItem analyzezdItem,
                                                        double analysisResult,
                                                        bool isLimitedByBudget)
        {
            return new FlipAnalyzedBazaarItem
            {
                ID = analyzezdItem.ID,
                Name = analyzezdItem.Name,
                SellPrice = analyzezdItem.SellPrice,
                SellVolume = analyzezdItem.SellVolume,
                SellMovingWeek = analyzezdItem.SellMovingWeek,
                SellOrders = analyzezdItem.SellOrders,
                BuyPrice = analyzezdItem.BuyPrice,
                BuyVolume = analyzezdItem.BuyVolume,
                BuyMovingWeek = analyzezdItem.BuyMovingWeek,
                BuyOrders = analyzezdItem.BuyOrders,
                BuyOrderList = analyzezdItem.BuyOrderList,
                SellOrderList = analyzezdItem.SellOrderList,
                PotentialHourlyProfit = analysisResult,
                IsLimitedByBudget = isLimitedByBudget
            };
        }
    }
}
