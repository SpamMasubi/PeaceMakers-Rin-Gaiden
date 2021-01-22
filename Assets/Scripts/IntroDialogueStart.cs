using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDialogueStart : MonoBehaviour
{
    public GameObject introBegin;

    public static bool playerInTrigger;

    // Start is called before the first frame update
    void Start()
    {
        introBegin.SetActive(true);
        playerInTrigger = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DialogueManager.isDialogueDone)
        {
            playerInTrigger = false;
            introBegin.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        Player _player = other.GetComponent<Collider2D>().GetComponent<Player>();
        if (_player != null)
        {
            playerInTrigger = true;
        }

    }
}
