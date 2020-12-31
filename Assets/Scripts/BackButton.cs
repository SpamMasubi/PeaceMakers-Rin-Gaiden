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
        SceneManager.LoadScene("Main Menu");
    }

    public void OnMouseOver()
    {
        sfxMan.selectionHover.Play();
    }
}
