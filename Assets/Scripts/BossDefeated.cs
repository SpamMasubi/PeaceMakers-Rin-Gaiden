using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeated : MonoBehaviour
{
    private bool playerWin = false;

    public string levelToLoad;

    public GameObject bossLevelUI;

    public static bool isWin = false;

    private SFXManager sfxMan;
    public GameObject startMessage;
    private bool canContinue = false;
    public float startDelay = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();
        isWin = false;
        playerWin = false;
        canContinue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && isWin && canContinue)
        {
            bossLevelUI.SetActive(false);
            startMessage.SetActive(false);
            isWin = false;
            sfxMan.playerStart.Play();
            SceneManager.LoadSceneAsync(levelToLoad);
            playerWin = false;
            BossHealth.isBossDead = false;
            CommanderHealth.isBossDead = false;
            TsukimiHealth.isBossDead = false;
            XelciorHealth.isBossDead = false;
            ManaHealth.isBossDead = false;
            HannaHealth.isBossDead = false;
            DialogueManager.isDialogueDone = false;
            canContinue = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = other.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
            StartCoroutine(activeStart());
            playerWin = true;
            bossLevelUI.SetActive(true);
            isWin = true;
            sfxMan.playerBossWin.Play();
            MusicController.musicCanPlay = false;
        }
    }

    public IEnumerator activeStart()
    {
        yield return new WaitForSeconds(startDelay);
        startMessage.SetActive(true);
        canContinue = true;
    }
}
