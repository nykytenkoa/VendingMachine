using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class IngredientStore
{
    public int Water { get; set; }
    public int Coffee { get; set; }
    public int Tea { get; set; }
    public int Sugar { get; set; }
    private const string FileName = "ingredients.txt";
    public IngredientStore()
    {
        Load();
    }
    public void Load()
    {
        if (File.Exists(FileName))
        {
            string[] lines = File.ReadAllLines(FileName);
            Water = int.Parse(lines[0]);
            Coffee = int.Parse(lines[1]);
            Tea = int.Parse(lines[2]);
            Sugar = int.Parse(lines[3]);
        }
        else
        {
            Water = 1000;
            Coffee = 500;
            Tea = 500;
            Sugar = 300;
        }
    }
    public void Save()
    {
        File.WriteAllLines(FileName, new[]
        {
            Water.ToString(),
            Coffee.ToString(),
            Tea.ToString(),
            Sugar.ToString()
        });
    }
}
