﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    //public AudioSource playerHurt;
    public AudioSource playerDead;
    public AudioSource playerAttack;
    public AudioSource playerYoyoAttack;
    public AudioSource playerJump;
    public AudioSource playerStart;
    public AudioSource playerWin;
    public AudioSource playerBossWin;
    public AudioSource playerRetry;
    public AudioSource spawning;

    public AudioSource selectionHover;
    public AudioSource selection;

    public AudioSource enemyDead;
    public AudioSource enemyHurt;
    public AudioSource enemyExplode;

    public AudioSource bossDead;
    public AudioSource bossHurt;
    public AudioSource bossAttack;
    public AudioSource bossAttackSFX;
    public AudioSource bossChargeUP;
    public AudioSource bossEnrageAttack;
    public AudioSource bossEnrage;

    public AudioSource commanderDeath;
    public AudioSource commanderEnrage;
    public AudioSource commanderHurt;
    public AudioSource commanderAttack;
    public AudioSource commanderAttackEnraged;

    public AudioSource Level2BossVoice;
    public AudioSource banditVoice;

    public AudioSource weaponSwing;
    public AudioSource points;
    public AudioSource healthItems;
    public AudioSource player1UP;
    public AudioSource beginGame;
    public AudioSource gunShotMultiple;
    public AudioSource rifleShots;

    private static bool sfxManagerExist;

    // Use this for initialization
    void Start () {

        if (!sfxManagerExist)
        {
            sfxManagerExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
