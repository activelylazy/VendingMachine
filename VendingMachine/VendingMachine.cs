using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    class VendingMachine
    {
        private readonly ICalculateChange _calculateChange;

        public VendingMachine()
            : this(new CalculateChange())
        { }

        public VendingMachine(ICalculateChange calculateChange)
        {
            _calculateChange = calculateChange;
            ReturnedCoins = new List<string>();
            DispensedProducts = new List<Product>();
        }

        public int Total { get; private set; }
        public IList<string> ReturnedCoins { get; private set; }
        public IList<Product> DispensedProducts { get; private set; }
        public string Display { get; private set; }

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

        public void RequestProduct(Product product)
        {
            if (product == Product.Cola && Total >= 100)
                DispenseProduct(product, 100);
            else if (product == Product.Candy && Total >= 65)
                DispenseProduct(product, 65);
            else
                Display = "INSERT MORE COINS";
        }

        private void DispenseProduct(Product product, int cost)
        {
            Display = "THANK YOU";
            DispensedProducts.Add(product);
            ReturnedCoins = _calculateChange.ChangeFor(Total - cost)
                .Select(coin => coin.ToString())
                .ToList();
            Total = 0;
        }
    }
}