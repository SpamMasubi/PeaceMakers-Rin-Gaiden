﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;

    float nextTimeToSearch = 0;

    public static bool startBoss = false;

    Vector3 lastTargetPosition;

    void Awake()
    {
        startBoss = true; 
    }

    void Start()
    {
        lastTargetPosition = player.position;
    }

    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
        else
        {
            LookAtPlayer();
        }

        LookAtPlayer();

    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (player != null)
        {
            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }
        else
        {
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
            if (searchPlayer != null)
                player = searchPlayer.transform;
            nextTimeToSearch = Time.time + 0.2f;
        }
    }
}
