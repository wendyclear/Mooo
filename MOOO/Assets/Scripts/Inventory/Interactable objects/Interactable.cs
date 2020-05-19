using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
	[SerializeField]
	private string _name;
	[SerializeField]
	private int _maxStack;
	[SerializeField]
	private Sprite _sprite;
	[SerializeField]
	private int _amount;
	[SerializeField]
	private CanvasManager _canvasManager;
	[SerializeField]
	private UIInventory _UIInventory;
	[SerializeField]
	private bool _destroyOnInteract;

	private void Start()
	{
		_canvasManager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
		_UIInventory   = GameObject.Find("InventoryMenu").GetComponent<UIInventory>();
	}
	public void ShowText()
	{
		_canvasManager.ShowMessage("[E] Pick up " + _name.ToString());
	}

	public void Interact()
	{
		if (_UIInventory.AddItem(_name, _sprite, _maxStack, _amount))
		{
			if (_destroyOnInteract) Object.Destroy(gameObject);
		}
		else
		{
			///Debug.Log("Inventory full! Can't pick up more items."); FIX
		}
	}

}