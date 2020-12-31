using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoadStage : MonoBehaviour
{
    private bool playerInZone;

    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        playerInZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInZone)
        {
            SceneManager.LoadSceneAsync(levelToLoad);
            playerInZone = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Player _player = other.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
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
