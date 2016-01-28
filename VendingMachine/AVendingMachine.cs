using System.Collections.Generic;
using TestFirst.Net;
using TestFirst.Net.Matcher;

namespace VendingMachine
{
    internal class AVendingMachine : PropertyMatcher<VendingMachine>
    {
        private static readonly VendingMachine PropertyNames = null;

        private AVendingMachine() { }

        public static AVendingMachine With()
        {
            return new AVendingMachine();
        }

        public AVendingMachine Total(int total)
        {
            return Total(AnInt.EqualTo(total));
        }

        public AVendingMachine Total(IMatcher<int?> total)
        {
            WithProperty(() => PropertyNames.Total, total);
            return this;
        }

        public AVendingMachine ReturnedCoins(params string[] coins)
        {
            return ReturnedCoins(AList.InOrder().WithOnlyValues(coins));
        }

        public AVendingMachine ReturnedCoins(IMatcher<IList<string>> coins)
        {
            WithProperty(() => PropertyNames.ReturnedCoins, coins);
            return this;
        }

        public AVendingMachine DispensedProducts(params Product[] products)
        {
            return DispensedProducts(AList.InOrder().WithOnlyValues(products));
        }

        public AVendingMachine DispensedProducts(IMatcher<IList<Product>> products)
        {
            WithProperty(() => PropertyNames.DispensedProducts, products);
            return this;
        }

        public AVendingMachine Display(string display)
        {
            return Display(AString.EqualTo(display));
        }

        public AVendingMachine Display(IMatcher<string> display)
        {
            WithProperty(() => PropertyNames.Display, display);
            return this;
        }
    }
}