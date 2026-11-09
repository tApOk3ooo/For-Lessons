using System.Collections.Generic;
using UnityEngine;

public class Burger
{
    private string _cheese = "сыр";
    private string _ketchup = "кетчуп";
    private string _cutlet = "котлета";
    private string _cucumber = "огурец";
    private string _salad = "салат";
    private string _onion = "лук";
    private string _specialSouce = "специальный соус";
    private string _sesameBun = "булочка с кунжутом";
    private string _doubleCutlet = "две котлеты";


    private string TryGetBurger(string _income1, string _income2, string _income3, string _income4, out string _burgerName)
    {

        _burgerName = "безыимянный бургер";

        string[] incoming = { _cucumber, _salad, _onion };

        List<string> cheeseBurgerRecipe = new List<string> { "сыр", "кетчуп", "котлета", "огурец" };
        List<string> bigMac = new List<string> { "огурец", "специальный соус", "булочка с кунжутом", "две котлеты" };

        List<string> checkList = new List<string>(cheeseBurgerRecipe);

        return _burgerName;
    }
}