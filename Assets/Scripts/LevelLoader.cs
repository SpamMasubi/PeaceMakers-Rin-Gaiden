using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public string loadLevel;
    public Slider slider;
    public Text progressText;
    void Start()
    {
        //start a sync Operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //create async Operation
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(loadLevel);
        while(!gameLevel.isDone)
        {
            //take progress bar fill = async operation progress
            float progress = Mathf.Clamp01(gameLevel.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return new WaitForEndOfFrame();
        }
        
    }

}
