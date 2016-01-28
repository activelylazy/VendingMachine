using System.Collections.Generic;

namespace VendingMachine
{
    internal class CalculateChange : ICalculateChange
    {
        private static int[] Coins = {1, 2, 5, 10, 20, 50, 100};

        public IList<int> ChangeFor(int amount)
        {
            foreach (var coin in Coins)
                if (coin == amount)
                    return new[] {amount};
            return new List<int>();
        }
    }
}