using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageComplete : MonoBehaviour
{
    private SFXManager sfxMan;
    public string loadScene;

    public GameObject startMessage;
    private bool canContinue = false;
    public float startDelay = 5.0f;
    void Start()
    {
        MusicController.musicCanPlay = false;
        sfxMan = FindObjectOfType<SFXManager>();
        canContinue = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(activeStart());
        if (Input.GetKeyDown("space") && canContinue)
        {
            
            MusicController.musicCanPlay = true;
            Boss.startBoss = false;
            Commander.startCommanderBoss = false;
            sfxMan.beginGame.Play();
            SceneManager.LoadSceneAsync(loadScene);
            canContinue = false;
            startMessage.SetActive(false);
        }
    }

    public IEnumerator activeStart()
    {
        yield return new WaitForSeconds(startDelay);
        startMessage.SetActive(true);
        canContinue = true;
    }
}
