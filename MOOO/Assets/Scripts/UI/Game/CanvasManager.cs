using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public Text _itemText;


    public void ShowMessage(string msg)
    {
        _itemText.text = msg;
        _itemText.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        _itemText.gameObject.SetActive(false);
    }
}
