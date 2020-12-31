using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLevel : MonoBehaviour
{
    private bool playerInWinZone;

    public string levelToLoad;

    public GameObject winLevelUI;

    public static bool isWin = false;

    private SFXManager sfxMan;

    public GameObject startMessage;
    private bool canContinue = false;
    public float startDelay = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();
        playerInWinZone = false;
        isWin = false;
        canContinue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && isWin && canContinue)
        {
            winLevelUI.SetActive(false);
            startMessage.SetActive(false);
            MusicController.musicCanPlay = true;
            isWin = false;
            sfxMan.playerStart.Play();
            SceneManager.LoadSceneAsync(levelToLoad);
            playerInWinZone = false;
            canContinue = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = other.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
            StartCoroutine(activeStart());
            playerInWinZone = true;
            winLevelUI.SetActive(true);
            isWin = true;
            sfxMan.playerWin.Play();
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
