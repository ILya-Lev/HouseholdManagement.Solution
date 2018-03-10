using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Model.UtilityPayments;
using Xunit;

namespace Test.Model.UtilityPayments
{
	public class TariffTestsAgainstSetup
	{
		private readonly Tariff _tariff;

		public TariffTestsAgainstSetup()
		{
			_tariff = new Tariff();
		}

		[Fact]
		public void ActualPrice_FirstRangeIsLimitless_PreviousRangeTakeIntoAccount()
		{
			var consumed = 50;
			_tariff.Prices = new[]
			{
				new UtilityPrice(30, null, -0.123m),
				new UtilityPrice(0, 100, 0.91m),
				new UtilityPrice(100, null, 1.68m),
			};

			var totalPrice = _tariff.ActualPrice(consumed);

			var expected = 30 * .91m + 20 * (-.123m);
			totalPrice.Should().Be(expected);
		}

		[Fact]
		public void ActualPrice_FirstRangeUpperLimit_ConsumedTimesPrice()
		{
			var consumed = 100;
			_tariff.Prices = new[]
			{
				new UtilityPrice(0, 100, 0.91m),
				new UtilityPrice(100, null, 1.68m),
			};

			var totalPrice = _tariff.ActualPrice(consumed);

			var expected = 100 * .91m;
			totalPrice.Should().Be(expected);
		}

		[Fact]
		public void ActualPrice_WithinLastRange_PreviousRangesAndRemainingPart()
		{
			var consumed = 150;

			Action priceCalculation = () => _tariff.ActualPrice(consumed);

			priceCalculation.Should().Throw<InvalidOperationException>("Setup Tariff prices before utilization.");
		}

	}
}
