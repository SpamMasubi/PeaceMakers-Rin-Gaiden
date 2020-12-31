using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SFXManager sfxMan;
    public string loadScene;

    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>(); ;
    }
    public void startGame()
    {
        Debug.Log("Let's play");
        sfxMan.selection.Play();
        sfxMan.beginGame.Play();
        SceneManager.LoadScene(loadScene);
    }

    public void QuitGame()
    {
        Debug.Log("Thanks for playing! Good bye");
        sfxMan.selection.Play();
        Application.Quit();
    }

    public void OnMouseOver()
    {
        sfxMan.selectionHover.Play();
    }
}
