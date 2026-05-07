using System;
using System.Globalization;
using System.Collections.Generic;

public class BMIcalculator
{
    static double height;
    static double weight;
    static bool endOfLoop = false;
    static double BMI;
    static char choice;
    const char yes = 't';
    const char no = 'n';

    static Dictionary<string, double> weightUnits = new Dictionary<string, double>()
    {
        {"Kg", 1.0},
        {"Lbs", 0.4536},
        {"Stones", 6.35},
        {"Grams", 0.001}
    };
    static Dictionary<string, double> heightUnits = new Dictionary<string, double>()
    {
        {"M", 1.0},
        {"Yd", 0.9144},
        {"Inch", 0.0254},
        {"Cm", 0.01}
    };

    static double GetNumber(string communicate)
    {
        double result;
        while(true)
        {
            Console.WriteLine(communicate);
            string input = Console.ReadLine().Replace(',', '.');
            if(string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Blad: nie wprowadzono danych. Sprobuj ponownie.");
                continue;
            }

            bool isSuccesful = double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result);

            if(isSuccesful)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Blad: Nieprawidlowe dane. Sprobuj ponownie.");
            }
        }
    }


    public static void Main(string[] args)
    {
        Console.WriteLine ("KALKULATOR BMI");
        while(!endOfLoop)
        {
            height = 0;
            weight = 0;
            
            while(true)
            {
                char meters = 'a';
                char centimeters = 'b';
                char yards = 'c';
                char inches = 'd';
                char customHeightUnit = 'e';
                Console.WriteLine("Wybierz jednostke miary wzrostu:\n'a' - metry \n'b' - centymetry, \n'c' - jardy \n'd' - cale, \n'e' - wlasne");
                string userHeightUnitInput = Console.ReadLine().ToLower();

                if(!char.TryParse(userHeightUnitInput, out char userHeightUnit))
                {
                    Console.WriteLine("Wprowadzono nieprawidlowa jednostke miary wzrostu. Wybierz poprawna jednostke.");
                    continue;                    
                }


                if(userHeightUnit != meters && userHeightUnit != centimeters && userHeightUnit != yards && userHeightUnit != inches && userHeightUnit != customHeightUnit)
                {
                   Console.WriteLine("Wprowadzono nieprawidlowa jednostke miary wzrostu. Wybierz poprawna jednostke.");
                    continue; 
                }

                double userHeight = GetNumber("Podaj swoj wzrost w wybranej jednostce: ");

                
                if(userHeightUnit == meters)
                {
                    height = userHeight * heightUnits["M"];
                }
                else if(userHeightUnit == centimeters)
                {
                    height = userHeight * heightUnits["Cm"];
                }
                else if(userHeightUnit == yards)
                {
                    height = userHeight * heightUnits["Yd"];
                }
                else if(userHeightUnit == inches)
                {
                    height = userHeight * heightUnits["Inch"];
                }
                else if(userHeightUnit == customHeightUnit)
                {
                    double userHeightRatio = GetNumber("Podaj ile metrow ma twoja jednostka wysokosci: ");
                    height = userHeight * userHeightRatio;
                }
                break;
            }

            while(true)
            {
                char kilograms = 'a';
                char grams = 'b';
                char pounds = 'c';
                char stones = 'd';
                char customWeightUnit = 'e';
                Console.WriteLine("Wybierz jednostke miary wagi: \n'a' - kilogramy, \n'b' - gramy, \n'c' - funty \n'd' - kamienie, \n'e' - wlasne");
                string userWeightUnitInput = Console.ReadLine().ToLower();

                if(!char.TryParse(userWeightUnitInput, out char userWeightUnit))
                {
                    Console.WriteLine("Wprowadzono nieprawidlowa jednostke miary wagi. Wybierz poprawna jednostke.");
                    continue;                    
                }

                if (userWeightUnit != kilograms && userWeightUnit != grams && userWeightUnit != pounds && userWeightUnit != stones && userWeightUnit != customWeightUnit)
                {
                    Console.WriteLine("Wprowadzono nieprawidlowa jednostke miary wagi. Wybierz poprawna jednostke.");
                    continue;
                }

                double userWeight = GetNumber("Podaj swoja wage (w wybranej jednostce): ");

                if(userWeightUnit == kilograms)
                {
                    weight = userWeight * weightUnits["Kg"];
                }
                else if(userWeightUnit == grams)
                {
                    weight = userWeight * weightUnits["Grams"];
                }
                else if(userWeightUnit == pounds)
                {
                    weight = userWeight * weightUnits["Lbs"];
                }
                else if(userWeightUnit == stones)
                {
                    weight = userWeight * weightUnits["Stones"];
                }
                else if(userWeightUnit == customWeightUnit)
                {
                    double userWeightRatio = GetNumber("Podaj ile kilogramow ma twoja jednostka wagi: ");
                    weight = userWeight * userWeightRatio;
                }
                break;
            }
            
            BMI = Math.Round(weight / (height * height), 2);

            Console.WriteLine("Twoje BMI wynosi: " + BMI);

            switch(BMI)
            {
                case < 18.5:
                    Console.WriteLine("Twoja kategoria BMI: Niedowaga.\n\n");
                    break;
                case >= 18.5 and < 25:
                    Console.WriteLine("Twoja kategoria BMI: Waga prawidlowa.\n\n");
                    break;
                case >= 25 and < 30:
                    Console.WriteLine("Twoja kategoria BMI: Nadwaga.\n\n");
                    break;
                case >= 30:
                    Console.WriteLine("Twoja kategoria BMI: Otylosc.\n\n");
                    break;
            }
            Console.WriteLine("Czy chcesz podac nowe dane? 't' - tak, 'n' - nie: ");
            while(true)
            {
                string inputChoice = Console.ReadLine();

                if (!char.TryParse(inputChoice, out choice))
                {
                        Console.WriteLine("Nie wprowadzono znaku. Podaj znak ponownie.\n");
                        continue;
                }
                switch (choice)
                {
                    case yes:
                        break;
                    case no:
                        endOfLoop = true;
                        break;
                    default:
                        Console.WriteLine("Nieprawidlowy wybor znaku. Podaj znak ponownie.\n");
                        continue;
                } 
                break;
            }
        }
    }
}
