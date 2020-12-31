using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private SFXManager sfxMan;
    public string loadScene;
    public string loadMainMenu;

    void Start()
    {
        MusicController.musicCanPlay = false;
        sfxMan = FindObjectOfType<SFXManager>();
    }

    public void Quit()
    {
        Debug.Log("Application Quit!");
        GameMaster.playGame = false;
        MusicController.musicCanPlay = true;
        GameMaster.isGameOver = false;
        GameMaster.charaDead = false;
        DialogueManager.isDialogueDone = false;
        Boss.startBoss = false;
        Commander.startCommanderBoss = false;
        sfxMan.selection.Play();
        SceneManager.LoadScene(loadMainMenu);
    }

    public void Retry()
    {
        Debug.Log("Let's play again");
        GameMaster.playGame = false;
        MusicController.musicCanPlay = true;
        GameMaster.isGameOver = false;
        GameMaster.charaDead = false;
        DialogueManager.isDialogueDone = false;
        Boss.startBoss = false;
        Commander.startCommanderBoss = false;
        sfxMan.selection.Play();
        sfxMan.playerRetry.Play();
        SceneManager.LoadScene(loadScene);
    }
    public void OnMouseOver()
    {
        sfxMan.selectionHover.Play();
    }
}
