using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartXelciorBoss : MonoBehaviour
{
    public GameObject introBegin;

    public static bool playerInTrigger;

    private SFXManager sfxMan;
    // Start is called before the first frame update
    void Start()
    {
        introBegin.SetActive(true);
        playerInTrigger = false;
        sfxMan = FindObjectOfType<SFXManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Player _player = other.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
            playerInTrigger = true;
            sfxMan.finalBossBegin.Play();
            introBegin.SetActive(false);
        }

    }
}
