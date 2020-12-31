using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1UpLife : MonoBehaviour
{
    public int lifeValue = 1;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameMaster.GetPlayerLives(this);
        }
    }
}
