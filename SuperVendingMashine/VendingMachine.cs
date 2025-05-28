using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

public class VendingMachine
{
    private ILog Log = LogManager.GetLogger(typeof(VendingMachine));
    public IngredientStore ingredients = new IngredientStore();
    public Dictionary<DrinkType, Drink> drinks;
    public Dictionary<DrinkType, int> prices;
    public int cash = 0;
    private const string priceFile = "prices.txt";
    private const string cashFile = "cash.txt";
    public VendingMachine(ILog log = null)
    {
       
        if (Log != null)
        {
            Log = log;
        }

        LoadPrices();
        LoadCash();
        drinks = new Dictionary<DrinkType, Drink>
        {
            { DrinkType.Coffee, new Drink(DrinkType.Coffee, 200, 20, 0, 10, prices[DrinkType.Coffee]) },
            { DrinkType.Tea, new Drink(DrinkType.Tea, 200, 0, 20, 10, prices[DrinkType.Tea]) },
            { DrinkType.Cappuccino, new Drink(DrinkType.Cappuccino, 250, 25, 0, 15, prices[DrinkType.Cappuccino]) }
        };
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n HOT DRINKS VENDING MACHINE");
            Console.WriteLine("1 - User Mode");
            Console.WriteLine("2 - Admin Mode");
            Console.WriteLine("0 - Exit");
            Console.Write("Choose mode: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1": UserMode(); break;
                case "2": AdminMode(); break;
                case "0": Save(); return;
                default: Console.WriteLine("Invalid input."); break;
            }
        }
    }
    public void UserMode()
    {
        Console.WriteLine("\n USER MODE ");
        foreach (var d in drinks)
        {
            Console.WriteLine($"{(int)d.Key + 1} - {d.Key} ({d.Value.Price} UAH)");
        }
        Console.Write("Choose drink (1-3): ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
        {
            DrinkType type = (DrinkType)(choice - 1);
            Drink drink = drinks[type];
            Console.Write($"Insert money (UAH): ");
            if (int.TryParse(Console.ReadLine(), out int money))
            {
                if (money < drink.Price)
                {
                    Console.WriteLine("Not enough money.");
                    return;
                }
                if (CheckIngredients(drink))
                {
                    UseIngredients(drink);
                    cash += drink.Price;
                    Console.WriteLine($"Enjoy your {type}! Change: {money - drink.Price} UAH");
                }
                else
                {
                    Console.WriteLine("Not enough ingredients.");
                }
            }
        }
    }
    public void AdminMode()
    {
        Console.WriteLine("\n ADMIN MODE ");
        while (true)
        {
            Console.WriteLine("1 - Refill ingredients");
            Console.WriteLine("2 - Show ingredients");
            Console.WriteLine("3 - Change prices");
            Console.WriteLine("4 - Withdraw cash");
            Console.WriteLine("0 - Back");
            Console.Write("Select option: ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1": Refill(); break;
                case "2": ShowIngredients(); break;
                case "3": ChangePrices(); break;
                case "4": WithdrawCash(); break;
                case "0": return;
                default: Console.WriteLine("Invalid input."); break;
            }
        }
    }
    public void Refill()
    {
        Console.Write("Add water (ml): ");
        ingredients.Water += int.Parse(Console.ReadLine());
        Console.Write("Add coffee (g): ");
        ingredients.Coffee += int.Parse(Console.ReadLine());
        Console.Write("Add tea (g): ");
        ingredients.Tea += int.Parse(Console.ReadLine());
        Console.Write("Add sugar (g): ");
        ingredients.Sugar += int.Parse(Console.ReadLine());
        Console.WriteLine("Ingredients updated.");
    }
    public void ShowIngredients()
    {
        Console.WriteLine("\nCurrent ingredients:");
        Console.WriteLine($"Water: {ingredients.Water} ml");
        Console.WriteLine($"Coffee: {ingredients.Coffee} g");
        Console.WriteLine($"Tea: {ingredients.Tea} g");
        Console.WriteLine($"Sugar: {ingredients.Sugar} g");
        Console.WriteLine($"Cash: {cash} UAH");
    }
    public void ChangePrices()
    {
        foreach (var type in Enum.GetValues(typeof(DrinkType)))
        {
            Console.Write($"New price for {type} (UAH): ");
            int newPrice = int.Parse(Console.ReadLine());
            prices[(DrinkType)type] = newPrice;
            drinks[(DrinkType)type].Price = newPrice;
        }
        Console.WriteLine("Prices updated.");
    }
    public void WithdrawCash()
    {
        Console.WriteLine($"Withdrawn {cash} UAH.");
        cash = 0;
    }
    public bool CheckIngredients(Drink drink)
    {
        return ingredients.Water >= drink.Water &&
               ingredients.Coffee >= drink.Coffee &&
               ingredients.Tea >= drink.Tea &&
               ingredients.Sugar >= drink.Sugar;
    }
    public void UseIngredients(Drink drink)
    {
        try
        {
            ingredients.Water -= drink.Water;
            ingredients.Coffee -= drink.Coffee;
            ingredients.Tea -= drink.Tea;
            ingredients.Sugar -= drink.Sugar;
        } catch(Exception e)
        {
            Log.Error(e.Message);
        }

    }
    public void LoadPrices()
    {
        prices = new Dictionary<DrinkType, int>();
        if (File.Exists(priceFile))
        {
            string[] lines = File.ReadAllLines(priceFile);
            prices[DrinkType.Coffee] = int.Parse(lines[0]);
            prices[DrinkType.Tea] = int.Parse(lines[1]);
            prices[DrinkType.Cappuccino] = int.Parse(lines[2]);
        }
        else
        {
            prices[DrinkType.Coffee] = 25;
            prices[DrinkType.Tea] = 20;
            prices[DrinkType.Cappuccino] = 30;
        }
    }
    public void LoadCash()
    {
        if (File.Exists(cashFile))
        {
            cash = int.Parse(File.ReadAllText(cashFile));
        }
        else
        {
            cash = 0;
        }
    }
    private void Save()
    {
        ingredients.Save();
        File.WriteAllLines(priceFile, new[]
        {
            prices[DrinkType.Coffee].ToString(),
            prices[DrinkType.Tea].ToString(),
            prices[DrinkType.Cappuccino].ToString()
        });
        File.WriteAllText(cashFile, cash.ToString());
    }
}





