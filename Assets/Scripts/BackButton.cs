using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    private SFXManager sfxMan;

    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>(); ;
    }
    public void BackMainMenu()
    {
        sfxMan.selection.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void FinishCredits()
    {
        sfxMan.selection.Play();
        MusicController.musicCanPlay = true;
        SceneManager.LoadScene("Main Menu");
    }

    public void SkipToMainMenu()
    {
        sfxMan.selection.Play();
        Time.timeScale = 1;
        CutsceneDialogue.isDialogueDone = true;
        startCutscene.beginCutscene = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void OnMouseOver()
    {
        sfxMan.selectionHover.Play();
    }
}
