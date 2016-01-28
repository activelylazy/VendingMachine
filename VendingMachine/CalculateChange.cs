using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    internal class CalculateChange : ICalculateChange
    {
        private static readonly int[] Coins = {1, 2, 5, 10, 20, 50, 100};

        public IList<int> ChangeFor(int amount)
        {
            foreach (var coin in Coins.OrderByDescending(i => i))
                if (amount >= coin)
                    return new[] {coin}.Concat(ChangeFor(amount - coin)).ToList();
            return new List<int>();
        }
    }
}