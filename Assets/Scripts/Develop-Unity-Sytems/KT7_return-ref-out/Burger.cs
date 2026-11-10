using System;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    private const string Cheese = "сыр";
    private const string Ketchup = "кетчуп";
    private const string Cutlet = "котлета";
    private const string Cucumber = "огурец";
    private const string Salad = "салат";
    private const string Onion = "лук";
    private const string SpecialSouce = "специальный соус";
    private const string SesameBun = "булочка с кунжутом";
    private const string DoubleCutlet = "две котлеты";
    private const string CheeseBurger = "чизбургер";
    private const string BigMac = "биг мак";
    private const string NotBurger = "не бургер";

    private void Start()
    {
        Burger maker = new Burger();

        if (TryMakeBurger(Onion, Salad, DoubleCutlet, Cucumber, out string result))
        {
            print($"вы успешно собрали этот бурхеер: <color=green>{result}</color>");
        }
        //if (TryMakeBurger(SpecialSouce, SesameBun, DoubleCutlet, Cucumber, out string result))
        //{
        //    print($"вы успешно собрали этот бурхеер: <color=green>{result}</color>");
        //}
        //if (TryMakeBurger(Cheese, Ketchup, Cutlet, Cucumber, out string result))
        //{
        //    print($"вы успешно собрали этот бурхеер: <color=green>{result}</color>");
        //}
        else
        {
            print($"рецепт не подошёл: <color=red>{result}</color>");
        }  
    }

    private bool TryMakeBurger(string income1, string income2, string income3, string income4, out string burgerName)
    {

        burgerName = NotBurger;

        string[] incoming = { income1, income2, income3, income4 };

        string[] cheeseBurgerRecipe = { Cheese, Ketchup, Cutlet, Cucumber };
        string[] bigMacRecipe = { Cucumber, SpecialSouce, SesameBun, DoubleCutlet };

        if (IsRecipeMatch(incoming, cheeseBurgerRecipe))
        {
            burgerName = CheeseBurger;
            return true;
        }
        else if (IsRecipeMatch(incoming, bigMacRecipe))
        {
            burgerName = BigMac;
            return true;
        }

        return false;
    }

    private bool IsRecipeMatch(string[] incoming, string[] recipe)
    {
        string[] sortedIncoming = (string[])incoming.Clone();
        string[] sortedRecipe = (string[])recipe.Clone();

        Array.Sort(sortedIncoming);
        Array.Sort(sortedRecipe);

        for (int i = 0; i < sortedIncoming.Length; i++)
        {
            if (sortedIncoming[i].ToLower() != sortedRecipe[i].ToLower())
            {
                return false;
            }
        }

        return true;
    }
}