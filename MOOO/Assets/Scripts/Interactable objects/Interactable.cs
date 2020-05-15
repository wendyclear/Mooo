using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
	public string Name;
	private GameObject _canvasManager;

	public void Start()
	{
		_canvasManager = GameObject.Find("CanvasManager");
	}


	public void ShowText()
	{
		_canvasManager.GetComponent<CanvasManager>().ShowMessage("[E] Pick up " + Name.ToString());
	}

	public void Interact()
	{
		//Debug.Log("FPSController - interacting with item of id :" + Id);
	}

}