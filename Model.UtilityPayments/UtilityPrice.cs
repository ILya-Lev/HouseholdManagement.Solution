using System.Diagnostics;

namespace Model.UtilityPayments
{
	[DebuggerDisplay("[{From}; {To}] ${Price}")]
	public class UtilityPrice
	{
		public UtilityPrice(double from, double? to, decimal price)
		{
			From = from;
			To = to;
			Price = price;
		}

		public double From { get; }
		public double? To { get; private set; }
		public decimal Price { get; }

		public void ChangeUpperBound(double? newTo) => To = newTo;
	}
}