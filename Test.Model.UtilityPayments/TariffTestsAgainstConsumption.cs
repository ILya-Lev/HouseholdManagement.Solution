using System;
using System.Linq;
using FluentAssertions;
using Model.UtilityPayments;
using Xunit;

namespace Test.Model.UtilityPayments
{
	public class TariffTestsAgainstConsumption
	{
		private readonly Tariff _tariff;

		public TariffTestsAgainstConsumption()
		{
			_tariff = new Tariff
			{
				Prices = new[]
				{
					new UtilityPrice(0, 100, 0.91m),
					new UtilityPrice(100, null, 1.68m),
				}
			};
		}

		[Fact]
		public void ActualPrice_WithinFirstRange_ConsumedTimesPrice()
		{
			var consumed = 50;

			var totalPrice = _tariff.ActualPrice(consumed);

			var expected = consumed * _tariff.Prices.First().Price;
			totalPrice.Should().Be(expected);
		}

		[Fact]
		public void ActualPrice_FirstRangeUpperLimit_ConsumedTimesPrice()
		{
			var consumed = 100;

			var totalPrice = _tariff.ActualPrice(consumed);

			var expected = consumed * _tariff.Prices.First().Price;
			totalPrice.Should().Be(expected);
		}

		[Fact]
		public void ActualPrice_WithinLastRange_PreviousRangesAndRemainingPart()
		{
			var consumed = 150;

			var totalPrice = _tariff.ActualPrice(consumed);

			var sumOverPreviousRanges = _tariff.Prices
				.Where((p, idx) => idx != _tariff.Prices.Count - 1)
				.Select(p => (decimal) (p.To - p.From) * p.Price)
				.Sum();

			var remainingPart = (decimal)(consumed - _tariff.Prices.Last().From) * _tariff.Prices.Last().Price;

			var expected = sumOverPreviousRanges + remainingPart;
			totalPrice.Should().Be(expected);
		}

		[Fact]
		public void ActualPrice_Zero_Zero()
		{
			var consumed = 0;

			var totalPrice = _tariff.ActualPrice(consumed);

			var expected = 0.0m;
			totalPrice.Should().Be(expected);
		}

		[Fact]
		public void ActualPrice_Negative_Zero()
		{
			var consumed = -50;

			var actualPrice = _tariff.ActualPrice(consumed);

			var expected = 0.0m;
			actualPrice.Should().Be(expected);
		}

		[Fact]
		public void ActualPrice_VeryLarge_DoNotCrash()
		{
			var consumed = 500_000_000_000_000L;

			var totalPrice = _tariff.ActualPrice(consumed);

			var sumOverPreviousRanges = _tariff.Prices
				.Where((p, idx) => idx != _tariff.Prices.Count - 1)
				.Select(p => (decimal) (p.To - p.From) * p.Price)
				.Sum();


			var remainingPart = (decimal)(consumed - _tariff.Prices.Last().From) * _tariff.Prices.Last().Price;

			var expected = sumOverPreviousRanges + remainingPart;
			totalPrice.Should().Be(expected);
		}
	}
}
