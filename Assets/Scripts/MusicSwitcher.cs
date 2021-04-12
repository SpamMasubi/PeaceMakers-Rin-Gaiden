using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour {

    private MusicController theMC;

    public int newTrack;

    public bool switchOnStart;
	// Use this for initialization
	void Start () {

        theMC = FindObjectOfType<MusicController>();

        if (switchOnStart || GameOverUI.returnToCheckPoint)
        {

            theMC.SwitchTrack(newTrack);
            gameObject.SetActive(false);
            GameOverUI.returnToCheckPoint = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Masaki")
        {
            theMC.SwitchTrack(newTrack);
            gameObject.SetActive(false);
        }
    }*/
}
