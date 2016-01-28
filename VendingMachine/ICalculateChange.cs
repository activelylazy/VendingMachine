using System.Collections.Generic;

namespace VendingMachine
{
    interface ICalculateChange
    {
        IList<int> ChangeFor(int amount);
    }
}