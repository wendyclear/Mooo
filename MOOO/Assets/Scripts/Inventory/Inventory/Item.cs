using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Item 
{
    private string _name;
    private Sprite _icon;
    private int _amount;
    private int _maxAmount;

    public Item()
    {
        _name      = null;
        _icon      = null;
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
    public void Reset()
    {
        _name      = null;
        _icon      = null;
        _amount    = 0;
        _maxAmount = 0;
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

    public void SetAmount(int amount)
    {
        _amount = amount;
    }


    public int GetMaxAmount()
    {
        return _maxAmount;
    }

    public void AddAmount(int amount)
    {
        _amount += amount;
    }

    public void RemoveAmount(int amount)
    {
        _amount -= amount;
    }

    public void SetMaxAmount()
    {
        _amount = _maxAmount;
    }

    public int Split()
    {
        int half = _amount / 2;
        _amount  = _amount / 2 + _amount % 2;
        return half;
    }

    public int Fit(int amount)
    {
        if (amount + _amount <= _maxAmount)
        {
            AddAmount(amount);
            return 0;
        }
        else
        {
            SetMaxAmount();
            return (amount + _amount - _maxAmount);
        }
    }

}
