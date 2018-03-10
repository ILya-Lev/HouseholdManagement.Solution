namespace Model.UtilityPayments
{
	public class UtilityPrice
	{
		public UtilityPrice(double from, double? to, decimal price)
		{
			From = from;
			To = to;
			Price = price;
		}

		public double From { get; }
		public double? To { get; }
		public decimal Price { get; }
	}
}