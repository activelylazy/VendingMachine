using System.Collections.Generic;

namespace VendingMachine
{
    class CalculateChange : ICalculateChange
    {
        public IList<int> ChangeFor(int amount)
        {
            return new[] {amount};
        }
    }
}