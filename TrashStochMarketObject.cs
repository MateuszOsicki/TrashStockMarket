using System;


namespace TrashStockMarket
{
    // Klasa Gracza - przechowuje stan i odpowiada za portfel/ekwipunek
    public class Player
    {
        public double Money { get; private set; }
        public int TrashAmount { get; private set; }


        public Player(double initialMoney, int initialTrashAmount)
        {
            Money = initialMoney;
            TrashAmount = initialTrashAmount;
        }


        public void BuyTrash(int amount, double totalCost)
        {
            Money -= totalCost;
            TrashAmount += amount;
        }


        public void SellTrash(int amount, double totalEarned)
        {
            Money += totalEarned;
            TrashAmount -= amount;
        }


        public void TransactionFailed(double tax)
        {
            Money -= tax;
        }


        public bool CanAfford(double cost) => Money >= cost;
        public bool HasEnoughTrash(int amount) => TrashAmount >= amount;
    }


    // Klasa Rynku - odpowiada za ceny i matematykę transakcji
    public class Market
    {
        public double Tax { get; } = 0.1;
        public double FixedSellPrice { get; } = 2.0;


        public double GenerateBuyPrice()
        {
            Random _rnd = new Random();
            return Math.Round(1.5 + _rnd.NextDouble(), 1);
        }


        public double CalculateBuyTransactionCost(double pricePerUnit, int amount)
        {
            return amount > 0 ? (pricePerUnit * amount) + Tax : 0;
        }


        public double CalculateSellTransactionRevenue(int amount)
        {
            return amount > 0 ? (FixedSellPrice * amount) - Tax : 0;
        }

        public double CalculateFailedTransactionCost(int amount)
        {
            return amount > 0 ? amount - Tax : 0;
        }
    }


    // Główny silnik gry
    public class Game
    {
        private readonly Player _player;
        private readonly Market _market;
        private const double WinningTarget = 50.0;


        public Game()
        {
            _player = new Player(10.0, 0);
            _market = new Market();
        }


        public void Start()
        {
            Console.WriteLine("GIELDA SMIECI");


            do
            {
                DisplayStatus();
                Console.WriteLine("Wybierz czy chcesz zakupic (z) czy sprzedac (s) smieci?");
                
                string input = Console.ReadLine()?.ToLower();
                if (string.IsNullOrEmpty(input)) continue;


                char action = input[0];


                if (action == 'z')
                {
                    HandleBuy();
                }
                else if (action == 's')
                {
                    HandleSell();
                }


            } while (!IsGameOver());


            DisplayResult();
        }


        private void DisplayStatus()
        {
            Console.WriteLine($"\nTwoj stan konta: {_player.Money:F2}$");
            Console.WriteLine($"Ilosc posiadanych smieci: {_player.TrashAmount}");
        }


        private void HandleBuy()
        {
            double currentPrice = _market.GenerateBuyPrice();

            Console.WriteLine($"Cena zakupu za sztuke wynosi: " + currentPrice + "$");

            while (true)
            {
                Console.WriteLine("Ile smieci chcesz zakupic?");

                
                if (!int.TryParse(Console.ReadLine(), out int amount))
                {
                    Console.WriteLine("Nieprawidlowa wartosc. Podaj liczbe calkowita. Proba kosztowala cie podatek.");
                    _player.TransactionFailed(_market.Tax);
                    continue;
                }
                
                if (amount < 0)
                {
                    Console.WriteLine("Nie mozna wprowadzac wartosci ujemnych. Podaj ilosc jeszcze raz. Proba kosztowala cie podatek.");
                    _player.TransactionFailed(_market.Tax);
                    continue;
                }

                double totalCost = _market.CalculateBuyTransactionCost(currentPrice, amount);

                if (_player.CanAfford(totalCost))
                {
                    _player.BuyTrash(amount, totalCost);
                    Console.WriteLine($"Zaplaciles: {totalCost:F2}$");
                    _player.TransactionFailed(_market.Tax);
                    break;
                }


                else
                {
                    Console.WriteLine("Masz za malo pieniedzy aby zakupic tyle smieci! Podaj ilosc jeszcze raz. Proba kosztowala cie podatek.");
                    _player.TransactionFailed(_market.Tax);
                }
            }
        }


        private void HandleSell()
        {
            Console.WriteLine($"Cena sprzedazy za sztuke wynosi: {_market.FixedSellPrice:F2}$");

            while (true)
            {
                Console.WriteLine("Ile smieci chcesz sprzedac?");
                
                if (!int.TryParse(Console.ReadLine(), out int amount))
                {
                    Console.WriteLine("Nieprawidlowa wartosc. Podaj liczbe calkowita. Proba kosztowala cie podatek.");
                    _player.TransactionFailed(_market.Tax);
                    continue;
                }


                if (amount < 0)
                {
                    Console.WriteLine("Nie mozna wprowadzac wartosci ujemnych. Podaj ilosc jeszcze raz. Proba kosztowala cie podatek.");
                    _player.TransactionFailed(_market.Tax);
                    continue;
                }


                else if (_player.HasEnoughTrash(amount))
                {
                    double totalEarned = _market.CalculateSellTransactionRevenue(amount);
                    _player.SellTrash(amount, totalEarned);
                    Console.WriteLine($"Zarobiles: {totalEarned:F2}$");
                    break;
                }
                else
                {
                    Console.WriteLine("Masz za malo smieci aby sprzedac taka ilosc! Podaj ilosc jeszcze raz. Proba kosztowala cie podatek.");
                    _player.TransactionFailed(_market.Tax);
                }
            }
        }


        private bool IsGameOver()
        {
            bool isBankrupt = _player.Money <= 0 && _player.TrashAmount == 0;
            bool isWinner = _player.Money >= WinningTarget;
            
            return isBankrupt || isWinner;
        }


        private void DisplayResult()
        {
            Console.WriteLine($"\nTwoj ostateczny stan konta: {_player.Money:F2}$");
            
            if (_player.Money >= WinningTarget)
            {
                Console.WriteLine("Wygrana! Jestes hegemonem smieci!!!");
            }
            else
            {
                Console.WriteLine("Przegrana! Zbankrutowales/as.");
            }
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}


