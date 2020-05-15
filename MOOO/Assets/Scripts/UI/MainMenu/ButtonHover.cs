using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{ 
    private Text _text;
    private float _rOff;
    private float _rOn;
    private float _gOff;
    private float _gOn;
    private float _bOff;
    private float _bOn;
    void Start()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Initialize();
        _text.color = new Color(_rOff / 255f, _gOff / 255f, _bOff / 255f);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = new Color(_rOn / 255f, _gOn / 255f, _bOn / 255f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = new Color(_rOff / 255f, _gOff / 255f, _bOff/ 255f);
    }

    private void Initialize()
    {
        _text = GetComponent<Text>();
        _rOff = 255;
        _gOff = 255;
        _bOff = 255;
        _rOn = 255;
        _gOn = 150;
        _bOn = 153;
        _text.color = new Color(_rOff / 255f, _gOff / 255f, _bOff / 255f);
    }

}
