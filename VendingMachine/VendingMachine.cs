namespace VendingMachine
{
    class VendingMachine
    {
        public int Total { get; private set; }

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
        }
    }
}