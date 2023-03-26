namespace Implementation.Services.Exstensions
{
    public static class TotalPriceExstensions
    {
        public static decimal Total(this decimal price, int quantity)
        {
            var subTotal = price * quantity;
            return Math.Round(subTotal, 2);
        }
    }
}
