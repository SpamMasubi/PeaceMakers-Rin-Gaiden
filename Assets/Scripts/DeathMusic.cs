using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMusic : MonoBehaviour
{
    public GameObject deathMusicObj;
    private static bool isDeathMusicExist;
    // Start is called before the first frame update
    void Start()
    {
        /*
        if (!isDeathMusicExist)
        {
            isDeathMusicExist = true;
            MusicController.musicCanPlay = true;
            charaDead = false;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/
        MusicController.musicCanPlay = true;
        deathMusicObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMaster.charaDead)
        { 
            deathMusicObj.SetActive(true);
            MusicController.musicCanPlay = false;
        }
        else
        {
            deathMusicObj.SetActive(false);
            MusicController.musicCanPlay = true;
        }
    }
}
