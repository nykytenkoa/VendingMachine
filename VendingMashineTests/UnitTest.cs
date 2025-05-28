using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Moq;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Sdk;
[TestClass]
public class VendingMachineTests
{

    [TestMethod]
    public void Refill_ShouldSucceed()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var input = new StringReader("1000\n500\n500\n300\n0");
        Console.SetIn(input);

        var machine = new VendingMachine();
        var initialWater = machine.ingredients.Water;
        var initialCoffee = machine.ingredients.Coffee;
        var initialTea = machine.ingredients.Tea;
        var initialSugar = machine.ingredients.Sugar;
        //act
        machine.Refill();
        //acert
        Assert.AreEqual(initialWater + 1000, machine.ingredients.Water);
        Assert.AreEqual(initialCoffee + 500, machine.ingredients.Coffee);
        Assert.AreEqual(initialTea + 500, machine.ingredients.Tea);
        Assert.AreEqual(initialSugar + 300, machine.ingredients.Sugar);

    }


    [TestMethod]
    public void CheckIngredients_ShouldSucceed()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var input = new StringReader("1000\n500\n500\n300\n0");
        Console.SetIn(input);

        var machine = new VendingMachine();
        machine.ingredients.Water = 5;
        machine.ingredients.Coffee = 10;
        machine.ingredients.Tea = 0;
        machine.ingredients.Sugar = 10;
      
        Assert.IsTrue(machine.CheckIngredients(new Drink(DrinkType.Coffee, 5, 10, 0, 2, 10)), "Failure to get coffee");
    }

    [TestMethod]
    public void UseIngridients_ShouldFailure()
    {

        var machine = new VendingMachine();

       
        Assert.ThrowsException<NullReferenceException>(() => machine.UseIngredients(null));
    }

    [TestMethod]
    public void BuyCoffee_ShouldSucceed()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var input = new StringReader("1\n1\n25\n0");
        Console.SetIn(input);


        var machine = new VendingMachine();

        machine.Run();

        Assert.IsTrue(output.ToString().Contains("Enjoy your Coffee! Change: 0 UAH"));
    }

    [TestMethod]
    public void BuyCoffee_ShouldFailure()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var input = new StringReader("1\n1\n20\n0");
        Console.SetIn(input);


        var machine = new VendingMachine();

        machine.Run();

        Assert.IsTrue(output.ToString().Contains("Not enough money."));
    }

    [TestMethod]
    public void BuyTea_ShouldSucceed()
    {
        var output = new StringWriter();
        Console.SetOut(output);
        var input = new StringReader("1\n2\n25\n0");
        Console.SetIn(input);

        var machine = new VendingMachine();
        machine.ingredients.Water = 1000;
        machine.ingredients.Coffee = 10000;
        machine.ingredients.Tea = 1000;
        machine.ingredients.Sugar = 10000;

        machine.Run();
        Assert.IsTrue(output.ToString().Contains("Enjoy your Tea! Change: 5 UAH"));
    }

}