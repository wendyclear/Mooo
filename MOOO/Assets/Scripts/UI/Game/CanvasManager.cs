using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private bool _gameOn;
    private GameObject _player;

    public Text _itemText;
    public Text _resultText;

    public GameObject _crosshairMenu;
    public GameObject _pauseMenu;
    public GameObject _itemMenu;
    public GameObject _finishMenu;

    void Start()
    {
        _gameOn = true;
        _player = GameObject.Find("FirstPersonPlayer");
        Time.timeScale = 1;
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
            _player.GetComponent<Player>().Pause(true);
            _gameOn = false;
            _crosshairMenu.SetActive(false);
            _itemText.gameObject.SetActive(false);
            _pauseMenu.SetActive(true);

        }
        else
        {
            _player.GetComponent<Player>().Pause(false);
            _gameOn = true;
            _crosshairMenu.SetActive(true);
            _pauseMenu.SetActive(false);
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
        _player.GetComponent<Player>().Pause(true);
        _crosshairMenu.SetActive(false);
        _itemMenu.SetActive(false);
        _pauseMenu.SetActive(false);
        if (win) _resultText.text = "YOU WON!";
        else _resultText.text = "YOU LOST!";
        _finishMenu.SetActive(true);
    }
}
