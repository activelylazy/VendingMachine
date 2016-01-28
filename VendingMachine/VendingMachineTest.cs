using NUnit.Framework;
using TestFirst.Net.Extensions.Moq;
using TestFirst.Net.Matcher;

namespace VendingMachine
{
    [TestFixture]
    public class VendingMachineTest : AbstractNUnitMoqScenarioTest
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
                        .ReturnedCoins(AList.InOrder().WithOnlyValues("3", "9"))));
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
                        .Total(0)
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

        [Test]
        public void GivesChange()
        {
            VendingMachine vendingMachine;
            ICalculateChange calculateChange;

            Scenario()
                .Given(calculateChange = AMock<ICalculateChange>()
                    .WhereMethod(c => c.ChangeFor(35)).Returns(new[] { 20, 10, 5 })
                    .Instance)
                .Given(vendingMachine = new VendingMachine(calculateChange))
                .Given(() => vendingMachine.AddCoin("100"))

                .When(() => vendingMachine.RequestProduct(Product.Candy))

                .Then(vendingMachine,
                    Is(AVendingMachine.With()
                        .Total(0)
                        .DispensedProducts(Product.Candy)
                        .Display("THANK YOU")
                        .ReturnedCoins("20", "10", "5")));
        }
    }
}