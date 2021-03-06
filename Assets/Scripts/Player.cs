using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }
        public void Init()
        {
            _curHealth = maxHealth;
        }
    }

    public PlayerStats stats = new PlayerStats();

    public int fallBoundary = -5;

    private SFXManager sfxMan;
    public static bool flashActive;
    public float flashLength;
    private float flashCounter;
    public Animator anim;
    private SpriteRenderer playerSprite;

    public AudioSource playerHurt;
    public AudioClip[] randHurt;
    //public string startPoint;

    public StatusIndicator statsInd;

    float delay = 0.4f;

    private Rigidbody2D rb2D;
    private float thrust = 500.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        sfxMan = FindObjectOfType<SFXManager>();
        playerHurt = GetComponent<AudioSource>();
        stats.Init();

        if (statsInd == null)
        {
            Debug.LogError("No Status Indicator reference on player");
        }
        else
        {
            if (!GameMaster.playGame)
            {
                GameMaster.playGame = true;
                statsInd.SetHealth(stats.curHealth, stats.maxHealth);
            }
        }
    }
    void Update()
    {
        if(transform.position.y <= fallBoundary)
        {
            DamagePlayer(999999);
        }
        
        if (flashActive)
        {

            if (flashCounter > flashLength * 2.64f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 2.31f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.98f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 1.65f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 1.32f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.99f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * 0.66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > 0.0f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }

            flashCounter -= Time.deltaTime;
        }
    }
    public void DamagePlayer(int damage)
    {
        if (!flashActive)
        {
            rb2D.AddForce(-transform.right * thrust);
            rb2D.velocity = Vector2.zero;
            anim.SetTrigger("playerHurt");
            stats.curHealth -= damage;
            flashActive = true;
            flashCounter = flashLength + 1.5f;
            playerHurt.clip = randHurt[Random.Range(0, randHurt.Length)];
            playerHurt.PlayOneShot(playerHurt.clip);
            if (stats.curHealth <= 0)
            {
                GameMaster.playGame = false;
                sfxMan.playerDead.Play();
                GameMaster.KillPlayer(this);
            }
            statsInd.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public void HealPlayer(int healthItem)
    {
        if (stats.curHealth < stats.maxHealth)
        {
            sfxMan.healthItems.Play();
            stats.curHealth += healthItem;
        }
        else
        {
            sfxMan.healthItems.Play();
            stats.curHealth = stats.maxHealth;
        }

        statsInd.SetHealth(stats.curHealth, stats.maxHealth);
    }
}
