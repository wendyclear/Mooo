using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
	public string Name;
	public int Id;
	private GameObject _canvasManager;

	public GameObject _player;

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
		//add to inventory id
		//_player.GetComponent<Player>().TakeDamage(120);
		Object.Destroy(gameObject);
	}

}