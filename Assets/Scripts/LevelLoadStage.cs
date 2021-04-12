using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoadStage : MonoBehaviour
{
    private bool playerInZone;

    public string levelToLoad;

    public static bool checkpoint = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInZone = false;
        checkpoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInZone)
        {
            if (MainMenu.secretStory)
            {
                HannaHealth.isBossDead = false;
                Hanna.startHannaBoss = false;
            }
            SceneManager.LoadSceneAsync(levelToLoad);
            checkpoint = true;
            playerInZone = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = other.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
            checkpoint = true;
            playerInZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player _player = other.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
            playerInZone = false;
        }
    }
}
