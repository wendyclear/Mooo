using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField]
    private Text _amount;
    [SerializeField]
    private Image _spriteImage;

    public int _order;

    public void SetOrder(int order)
    {
        _order = order;
    }

    public void SetCell(int amount, Sprite image)
    {
        _amount.text        = amount.ToString();
        _spriteImage.sprite = image;
    }

    public void ShowCell()
    {
        _amount.gameObject.SetActive(true);
        _spriteImage.gameObject.SetActive(true);
    }
    public void HideCell()
    {
        _amount.gameObject.SetActive(false);
        _spriteImage.gameObject.SetActive(false);
    }

    public int GetOrder()
    {
        return _order;
    }

}
