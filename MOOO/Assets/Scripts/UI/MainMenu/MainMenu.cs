using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject _mainCanvas;
    public GameObject _instructionsCanvas;
    public GameObject _buttons;
    public GameObject _loadingBar;

    public void OnButtonClick_ExitGame()
    {
        Application.Quit();
    }

    public void OnButtonClick_StartGame()
    {
        _buttons.SetActive(false);
        _loadingBar.SetActive(true);
        LoadGame();
    }

    public void OnButtonClick_OpenInstructions()
    {
        _mainCanvas.SetActive(false);
        _instructionsCanvas.SetActive(true);
    }

    public void OnButtonClick_CloseInstructions()
    {
        _mainCanvas.SetActive(true);
        _instructionsCanvas.SetActive(false);
    }

    private void LoadGame()
    {
        _loadingBar.GetComponent<LoadingBar>().LoadGame();
        //WSceneManager.LoadScene(1);
    }
}
