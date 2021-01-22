﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    private SFXManager sfxMan;

    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>(); ;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        MusicController.musicPause = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        MusicController.musicPause = true;
    }

    public void LoadLevelSelect()

    {
        Time.timeScale = 1f;
        GameMaster.playGame = false;
        Boss.startBoss = false;
        Commander.startCommanderBoss = false;
        DialogueManager.isDialogueDone = false;
        MusicController.musicCanPlay = true;
        MusicController.musicPause = false;
        sfxMan.selection.Play();
        SceneManager.LoadScene("LevelSelectScreen");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        GameMaster.playGame = false;
        Boss.startBoss = false;
        Commander.startCommanderBoss = false;
        DialogueManager.isDialogueDone = false;
        MusicController.musicCanPlay = true;
        MusicController.musicPause = false;
        sfxMan.selection.Play();
        SceneManager.LoadScene("Main Menu");
    }

    public void OnMouseOver()
    {
        sfxMan.selectionHover.Play();
    }

}