using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item 
{
    private string _name;
    private Sprite _icon;
    private int _amount;
    private int _maxAmount;

    public Item()
    {
        _name       = null;
        _icon       = null;
        _amount    = 0;
        _maxAmount = 0;
    }

    public void SetItem(string name, Sprite icon, int amount, int maxAmount)
    {
        _name      = name;
        _icon      = icon;
        _amount    = amount;
        _maxAmount = maxAmount;

    }

public string GetName()
    {
        return _name;
    }

    public Sprite GetSprite()
    {
        return _icon;
    }

    public int GetAmount()
    {
        return _amount;
    }


    public int GetMaxAmount()
    {
        return _maxAmount;
    }

    public void AddAmount(int amount)
    {
        _amount += amount;
    }



}
