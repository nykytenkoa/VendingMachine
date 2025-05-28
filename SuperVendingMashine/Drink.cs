using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Drink
{
    public DrinkType Type { get; set; }
    public int Water { get; set; }
    public int Coffee { get; set; }
    public int Tea { get; set; }
    public int Sugar { get; set; }
    public int Price { get; set; }
    public Drink(DrinkType type, int water, int coffee, int tea, int sugar, int price)
    {
        Type = type;
        Water = water;
        Coffee = coffee;
        Tea = tea;
        Sugar = sugar;
        Price = price;
    }
}
