using System;

public class StockMarket
{
    public static void Main(string[] args)
    {
        Console.WriteLine("GIELDA SMIECI");
        double money = 10;
        int trashAmount = 0;
        double tax = 0.1;
        int amount = 0;

        while (money < 50 && (money > 0 || trashAmount > 0))
        {
            Console.WriteLine("\nTwoj stan konta: " + Math.Round(money, 2) + "$");
            Console.WriteLine("Ilosc posiadanych smieci: " + trashAmount);
            Console.WriteLine("Wybierz czy chcesz zakupic (z) czy sprzedac (s) smieci?");
            
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;
            char action = input[0];

            if (action == 'z')
            {
                Random rnd = new Random();
                double price = Math.Round(1.5 + rnd.NextDouble(), 1);
                Console.WriteLine("Cena wynosi: " + price + "$");

                bool incorrectAmount = true;
                while (incorrectAmount)
                {
                    Console.WriteLine("Ile smieci chcesz zakupic?");
                    string inputAmount = Console.ReadLine();
                    if (!int.TryParse(inputAmount, out amount))
                    {
                        Console.WriteLine("To nie jest liczba! Proba kosztowala cie podatek.");
                        money -= tax;
                        continue; // Przeskakuje z powrotem do pytania "Ile smieci..."
                    }
                    if (amount < 0)
                    {
                        Console.WriteLine("Nie mozna wprowadzac wartosci ujemnych. Proba kosztowala cie podatek.");
                        money -= tax;
                    }
                    else
                    {
                        double totalCost = price * amount;
                        
                        if (money >= totalCost + tax)
                        {
                            money -= (totalCost + tax);
                            trashAmount += amount;
                            Console.WriteLine("Zakupiono " + amount + " szt. za " + totalCost + "$ + " + tax + "$ podatku.");
                            incorrectAmount = false;
                        }
                        else
                        {
                            Console.WriteLine("Za malo pieniedzy! Proba kosztowala Cie podatek.");
                            money -= tax;
                            incorrectAmount = false;
                        }
                    }
                    if (money <= 0 && trashAmount <= 0) break; 
                }
            }
            else if (action == 's')
            {
                double sellPrice = 2.0;
                bool incorrectAmount = true;
                while (incorrectAmount)
                {
                    Console.WriteLine("Ile smieci chcesz sprzedac?");
                    string inputAmount = Console.ReadLine();
                    if (!int.TryParse(inputAmount, out amount))
                    {
                        Console.WriteLine("To nie jest liczba! Proba kosztowala cie podatek.");
                        money -= tax;
                        continue; // Przeskakuje z powrotem do pytania "Ile smieci..."
                    }

                    if (amount < 0)
                    {
                        Console.WriteLine("Nie mozna wprowadzac wartosci ujemnych. Proba kosztowala cie podatek.");
                        money -= tax;
                    }
                    else if (amount > trashAmount)
                    {
                        Console.WriteLine("Nie masz tylu smieci! Proba kosztowala cie podatek.");
                        money -= tax;
                        incorrectAmount = false;
                    }
                    else if (amount == 0)
                    {
                        incorrectAmount = false;
                    }
                    else
                    {
                        double earned = sellPrice * amount;
                        money += (earned - tax);
                        trashAmount -= amount;
                        Console.WriteLine("Zarobiles: " + (earned - tax) + "$.");
                        incorrectAmount = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Nieznana komenda! Wybierz 'z' lub 's'.");
            }
        }

        if (money >= 50)
        {
            Console.WriteLine("\nTwoj ostateczny stan konta: " + Math.Round(money, 2) + "$");
            Console.WriteLine("Wygrana! Jestes hegemonem smieci!!!");
        }
        else
        {
            Console.WriteLine("\nPrzegrana! Zbankrutowales/as.");
        }
    }
}
