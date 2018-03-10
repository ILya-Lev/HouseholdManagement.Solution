using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.UtilityPayments
{
	public class Tariff
	{
		private IReadOnlyList<UtilityPrice> _prices;

		public IReadOnlyList<UtilityPrice> Prices
		{
			get => _prices ?? throw new InvalidOperationException("Setup Tariff prices before utilization.");
			set
			{
				_prices = value?.OrderBy(p => p.From).ThenBy(p => p.To ?? 0).ThenByDescending(p => p.Price).ToList()
				          ?? throw new ArgumentNullException("Tariff needs not null range of prices.");

				for (int i = 0; i + 1 < _prices.Count; i++)
				{
					if (_prices[i].To == null || _prices[i].To > _prices[i + 1].From)
						_prices[i].ChangeUpperBound(_prices[i + 1].From);
				}
			}
		}

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
