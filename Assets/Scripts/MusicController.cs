using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public static bool mcExists;

    public AudioSource[] musicTracks;

    public int currentTrack;

    public static bool musicCanPlay = true;
    public static bool musicPause = false;

    // Use this for initialization
    void Start () {

        if (!mcExists)
        {
            mcExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (musicCanPlay)
        {
            if (!musicTracks[currentTrack].isPlaying && !WinLevel.isWin && !BossDefeated.isWin)
            {
                musicTracks[currentTrack].Play();
            }

            if (musicPause)
            {
                musicTracks[currentTrack].Pause();
            }
            else if (!musicPause)
            {
                musicTracks[currentTrack].UnPause();
            }
        }else
        {
            musicTracks[currentTrack].Stop();
        }

    }

    public void SwitchTrack(int newTrack)
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].Play();
    }
}
