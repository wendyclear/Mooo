using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Text _percentage;
    public Slider _slider;
    private AsyncOperation _loading;
    public void LoadGame()
    {
        ResetLoading();
    }

    private void ResetLoading()
    {
        _percentage.text = "0%";
        StartCoroutine(Load());
    }

    private IEnumerator Load ()
    {
        _loading = SceneManager.LoadSceneAsync(1);
        while(!_loading.isDone)
        {
            UpdateLoading(_loading.progress);
            yield return null;
        }
        UpdateLoading(_loading.progress);
        _loading = null;
    }  
    
   private void UpdateLoading ( float progress)
    {
        _slider.value = progress;
        _percentage.text = (progress*100).ToString() + "%";
    }
}
