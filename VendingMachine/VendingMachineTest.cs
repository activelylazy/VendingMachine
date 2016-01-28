using NUnit.Framework;
using TestFirst.Net.Extensions.NUnit;
using TestFirst.Net.Matcher;

namespace VendingMachine
{
    [TestFixture]
    public class VendingMachineTest : AbstractNUnitScenarioTest
    {
        [Test]
        public void InitialValueIsZero()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())

                .Then(vendingMachine.Total, Is(AnInt.EqualTo(0)));
        }

        [Test]
        public void AddingCoinsIncrementsValue()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())

                .When(() => vendingMachine.AddCoin("1"))
                .When(() => vendingMachine.AddCoin("2"))
                .When(() => vendingMachine.AddCoin("5"))
                .When(() => vendingMachine.AddCoin("10"))
                .When(() => vendingMachine.AddCoin("20"))
                .When(() => vendingMachine.AddCoin("50"))
                .When(() => vendingMachine.AddCoin("100"))

                .Then(vendingMachine.Total, Is(AnInt.EqualTo(188)));
        }

        [Test]
        public void AddingUnrecognisedCoinAddsToReturnedCoins()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())

                .When(() => vendingMachine.AddCoin("2"))
                .When(() => vendingMachine.AddCoin("3"))
                .When(() => vendingMachine.AddCoin("9"))

                .Then(vendingMachine.Total, Is(AnInt.EqualTo(2)))
                .Then(vendingMachine.ReturnedCoins, 
                      Is(AList.InOrder().WithOnlyValues("3", "9")));
        }

        [Test]
        public void DispensesProductForExactMoney()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())
                .Given(() => vendingMachine.AddCoin("100"))

                .When(() => vendingMachine.RequestProduct(Product.Cola))

                .Then(vendingMachine.DispensedProducts,
                      Is(AList.InOrder().WithOnlyValues(Product.Cola)));
        }

        [Test]
        public void ShowsErrorIfNotEnoughMoneyAdded()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())

                .When(() => vendingMachine.RequestProduct(Product.Cola))

                .Then(vendingMachine.DispensedProducts, Is(AList.NoItems<string>()))
                .Then(vendingMachine.Display, Is(AString.EqualTo("INSERT MORE COINS")));
        }
    }
}