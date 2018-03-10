using System;
using System.Collections.Generic;

namespace Model.UtilityPayments
{
	public class Tariff
	{
		public IReadOnlyCollection<UtilityPrice> Prices { get; set; }

		public decimal ActualPrice(double consumed)
		{
			var totalPrice = 0.0m;
			foreach (var aPrice in Prices)
			{
				if (consumed <= aPrice.From) break;

				if (aPrice.To == null)
				{
					totalPrice += PriceOfRange(consumed, aPrice.From, aPrice.Price);
					break;
				}

				if (consumed <= aPrice.To)
				{
					totalPrice += PriceOfRange(consumed, aPrice.From, aPrice.Price);
				}
				else
				{
					totalPrice += PriceOfRange(aPrice.To.Value, aPrice.From, aPrice.Price);
				}
			}

			return totalPrice;

			decimal PriceOfRange(double upperBound, double lowerBound, decimal priceInRange)
			{
				return (decimal)(upperBound - lowerBound) * priceInRange;
			}
		}
	}
}
