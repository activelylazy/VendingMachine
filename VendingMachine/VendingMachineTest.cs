using NUnit.Framework;
using TestFirst.Net.Extensions.NUnit;
using TestFirst.Net.Matcher;

namespace VendingMachine
{
    [TestFixture]
    public class VendingMachineTest : AbstractNUnitScenarioTest
    {
        [Test]
        public void AddingPennyIncrementsValue()
        {
            VendingMachine vendingMachine;

            Scenario()
                .Given(vendingMachine = new VendingMachine())

                .When(() => vendingMachine.AddCoin("1"))

                .Then(vendingMachine.Total, Is(AnInt.EqualTo(1)));
        }
    }
}