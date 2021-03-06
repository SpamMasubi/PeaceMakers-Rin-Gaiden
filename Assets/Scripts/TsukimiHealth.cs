﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsukimiHealth : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int currentHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 10;

        public void Init()
        {
            currentHealth = maxHealth;
        }
    }

    public Animator anim;
    private SFXManager sfxMan;
    private bool flashActive;
    public float flashLength;
    private float flashCounter;
    public EnemyStats stats = new EnemyStats();
    private SpriteRenderer enemySprite;

    public int getScore = 10;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statsInd;

    public bool isInvulnerable = false;
    public static bool isEnrage = false;

    public static bool bossDamage = false;
    public static bool isBossDead = false;
    public GameObject winPrize;

    public static bool canUnlock = false;
    public static bool lvlComplete = false;

    void Start()
    {
        stats.Init();
        if (statsInd != null)
        {
            statsInd.SetHealth(stats.currentHealth, stats.maxHealth);
        }
        sfxMan = FindObjectOfType<SFXManager>();
        enemySprite = GetComponent<SpriteRenderer>();
        getScore = Random.Range(1, 100);
        isBossDead = false;
    }

    public void TakeDamage(int damage)
    {
        if (!isBossDead)
        {
            if (isInvulnerable)
            {
                return;
            }
            if (!flashActive)
            {
                if (isEnrage)
                {
                    GetComponent<Animator>().SetBool("Enrage Hurt", true);
                }
                anim.Play("TsukimiHurt");
                stats.currentHealth -= damage;
                flashActive = true;
                bossDamage = true;
                flashCounter = flashLength + 1.5f;
                sfxMan.tsukimiHurt.Play();

                if (stats.currentHealth <= 750 && !isInvulnerable && !isEnrage)
                {
                    isEnrage = true;
                    sfxMan.bossChargeUP.Play();
                    sfxMan.tsukimiEnrage.Play();
                    GetComponent<Animator>().SetBool("IsEnraged", true);
                }

                if (stats.currentHealth <= 0)
                {
                    isEnrage = false;
                    sfxMan.tsukimiDeath.Play();
                    Tsukimi.startTsukimiBoss = false;
                    GetComponent<Animator>().SetBool("isDefeated", true);
                    GameMaster.TsukimiDefeat(this);
                    winPrize.SetActive(true);
                    bossDamage = false;
                    isBossDead = true;
                    if (MainMenu.newGame)
                    {
                        canUnlock = true;
                        lvlComplete = true;
                    }
                    
                }

                if (statsInd != null)
                {
                    statsInd.SetHealth(stats.currentHealth, stats.maxHealth);
                }
            }
        }
        else
        {
            return;
        }

    }

    void Update()
    {
        if (flashActive && !isBossDead)
        {
            if (flashCounter > flashLength * 2.64f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 2.31)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.98f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 1.65f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.32f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 0.66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > 0.0f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }

    }
}