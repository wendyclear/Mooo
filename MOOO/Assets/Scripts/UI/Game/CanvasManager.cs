﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private bool _gameOn;

    [SerializeField]
    private Player _player; 

     [SerializeField]
    private Text _itemText;
    [SerializeField]
    private Text _resultText;
    [SerializeField]
    private Text _healthText;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Text _bucketText;

    [SerializeField]
    private GameObject _crosshairMenu;
    [SerializeField]
    private GameObject _pauseMenu;
    [SerializeField]
    private GameObject _itemMenu;
    [SerializeField]
    private GameObject _finishMenu;
    [SerializeField]
    private UIInventory _inventoryMenu;

    void Start()
    {
        _gameOn        = true;
        Time.timeScale = 1;
        ChangeHealth();
        ChangeAmmo();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
        if (Input.GetButtonDown("R"))
        {
            OnButtonClick_RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !_pauseMenu.activeSelf)
        {
            OpenInventory();
        }
    }
    public void ShowMessage(string msg)
    {
        _itemText.text = msg;
        _itemText.gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        _itemText.gameObject.SetActive(false);
    }

    private void Pause()
    {
        if (_gameOn)
        {
            _player.Pause(true);
            _gameOn = false;
            _crosshairMenu.SetActive(false);
            _itemText.gameObject.SetActive(false);
            _pauseMenu.SetActive(true);
            _inventoryMenu.CloseInventory();

        }
        else
        {
            _player.Pause(false);
            _gameOn = true;
            _crosshairMenu.SetActive(true);
            _pauseMenu.SetActive(false);
            _inventoryMenu.CloseInventory();
        }

    }
    public void OnButtonClick_ExitGame()
    {
        Application.Quit();
    }

    public void OnButtonClick_RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnButtonClick_BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver(bool win)
    {
        _gameOn = false;
        _player.Pause(true);
        _crosshairMenu.SetActive(false);
        _itemMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        if (win) _resultText.text = "YOU WON!";
        else _resultText.text = "YOU LOST!";
        _finishMenu.SetActive(true);
    }

    public void ChangeHealth()
    {
        _healthText.text = "Your health : " + Mathf.FloorToInt(_player.GetHealth()).ToString()+ " / "+ _player.GetMaxHealth().ToString();
    }
    public void ChangeAmmo()
    {
        _ammoText.text = "Your ammo : " + _inventoryMenu.CountOf("Ammo");
    }

    public void ChangeBuckets()
    {
        _bucketText.text = "Buckets collected : " + _inventoryMenu.CountOf("Bucket of milk");
    }

    public void OpenInventory()
    {
        if (_gameOn)
        {
            _player.Pause(true);
            _gameOn = false;
            _crosshairMenu.SetActive(false);
            _itemText.gameObject.SetActive(false);
            _pauseMenu.SetActive(false);
            _inventoryMenu.OpenInventory();
        }
        else
        {
            _gameOn = true;
            _player.Pause(false);
            _crosshairMenu.SetActive(true);
            _inventoryMenu.CloseInventory();
        }
    }
}
