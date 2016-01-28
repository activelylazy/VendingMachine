using System.Collections.Generic;

namespace VendingMachine
{
    class VendingMachine
    {
        public VendingMachine()
        {
            ReturnedCoins = new List<string>();
            DispensedProducts = new List<string>();
        }

        public int Total { get; private set; }
        public IList<string> ReturnedCoins { get; private set; }
        public IList<string> DispensedProducts { get; private set; } 

        public void AddCoin(string coin)
        {
            if (coin == "1")
                Total += 1;
            else if (coin == "2")
                Total += 2;
            else if (coin == "5")
                Total += 5;
            else if (coin == "10")
                Total += 10;
            else if (coin == "20")
                Total += 20;
            else if (coin == "50")
                Total += 50;
            else if (coin == "100")
                Total += 100;
            else
                ReturnedCoins.Add(coin);
        }

        public void RequestProduct(string productName)
        {
            if (productName == "cola")
                DispensedProducts.Add(productName);
        }
    }
}