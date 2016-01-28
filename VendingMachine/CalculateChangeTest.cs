using System.Collections.Generic;
using NUnit.Framework;
using TestFirst.Net.Extensions.NUnit;
using TestFirst.Net.Matcher;

namespace VendingMachine
{
    [TestFixture]
    public class CalculateChangeTest : AbstractNUnitScenarioTest
    {
        [Test]
        public void ChangeForZeroIsEmpty()
        {
            CalculateChange calculateChange;
            IList<int> result;

            Scenario()
                .Given(calculateChange = new CalculateChange())

                .When(result = calculateChange.ChangeFor(0))

                .Then(result, Is(AList.NoItems<int>()));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(50)]
        [TestCase(100)]
        public void ChangeForCoinIsACoin(int coin)
        {
            CalculateChange calculateChange;
            IList<int> result;

            Scenario()
                .Given(calculateChange = new CalculateChange())

                .When(result = calculateChange.ChangeFor(coin))

                .Then(result, Is(AList.InOrder().WithOnlyValues(coin)));
        }

        [TestCase(198, new[] { 100, 50, 20, 20, 5, 2, 1 })]
        [TestCase(35, new[] { 20, 10, 5 })]
        public void GivesChangeStartingFromLargestPossibleCoin(int amount, int[] coins)
        {
            CalculateChange calculateChange;
            IList<int> result;

            Scenario()
                .Given(calculateChange = new CalculateChange())

                .When(result = calculateChange.ChangeFor(amount))

                .Then(result, Is(AList.InOrder().WithOnlyValues(coins)));
        }
    }
}
