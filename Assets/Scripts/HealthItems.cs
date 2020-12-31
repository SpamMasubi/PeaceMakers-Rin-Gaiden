using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItems : MonoBehaviour
{
    public int healthValue = 10;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Collider2D>().GetComponent<Player>();
            if (player != null)
            {
                player.HealPlayer(healthValue);
            }

            Destroy(gameObject);
        }
    }
}
