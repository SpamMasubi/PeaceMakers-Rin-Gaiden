using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SFXManager sfxMan;

    public Button[] extraButtons;

    public string loadScene;

    public static bool newGame = false;

    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>(); ;
    }

    void FixedUpdate()
    {
        if (GameMaster.storyComplete)
        {
            extraButtons[0].gameObject.SetActive(true);
            extraButtons[1].gameObject.SetActive(true);
        }
    }

    public void startGame()
    {
        Debug.Log("Let's play");
        sfxMan.selection.Play();
        sfxMan.beginGame.Play();
        newGame = true;
        SceneManager.LoadScene(loadScene);
    }

    public void ReplayGame()
    {
        Debug.Log("Let's play");
        sfxMan.selection.Play();
        sfxMan.beginGame.Play();
        newGame = false;
        GameMaster.storyComplete = true;
        SceneManager.LoadScene(loadScene);
    }

    public void PlaySecretStory()
    {
        Debug.Log("Let's play");
        sfxMan.selection.Play();
        sfxMan.beginGame.Play();
        //SceneManager.LoadScene(loadScene);
    }

    public void QuitGame()
    {
        Debug.Log("Thanks for playing! Good bye");
        sfxMan.selection.Play();
        Application.Quit();
    }

    public void EndCredits()
    {
        Debug.Log("Credits");
        sfxMan.selection.Play();
        MusicController.musicCanPlay = false;
        SceneManager.LoadScene("End Credits");
    }


    public void OnMouseOver()
    {
        sfxMan.selectionHover.Play();
    }
}
