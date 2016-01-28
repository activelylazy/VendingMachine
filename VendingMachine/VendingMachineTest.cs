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

                .Then(vendingMachine,
                      Is(AVendingMachine.With()
                        .Total(0)
                        .DispensedProducts()
                        .Display(AString.Null())
                        .ReturnedCoins()));
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

                .Then(vendingMachine,
                    Is(AVendingMachine.With()
                        .Total(188)
                        .DispensedProducts()
                        .Display(AString.Null())
                        .ReturnedCoins()));
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

                .Then(vendingMachine,
                    Is(AVendingMachine.With()
                        .Total(2)
                        .DispensedProducts()
                        .Display(AString.Null())
                        .ReturnedCoins("3", "9")));
        }

        [Test]
        public void DispensesProductForExactMoney()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())
                .Given(() => vendingMachine.AddCoin("100"))

                .When(() => vendingMachine.RequestProduct(Product.Cola))

                .Then(vendingMachine,
                    Is(AVendingMachine.With()
                        //.Total(0)
                        .DispensedProducts(Product.Cola)
                        .Display("THANK YOU")
                        .ReturnedCoins()));
        }

        [Test]
        public void ShowsErrorIfNotEnoughMoneyAdded()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())

                .When(() => vendingMachine.RequestProduct(Product.Cola))

                .Then(vendingMachine,
                    Is(AVendingMachine.With()
                        .Total(0)
                        .DispensedProducts()
                        .Display("INSERT MORE COINS")
                        .DispensedProducts()));
        }
    }
}