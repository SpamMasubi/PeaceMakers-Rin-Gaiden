using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleEnemy : MonoBehaviour
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
    //private UnityEngine.Object explosionRef;

    private SpriteRenderer enemySprite;
    public EnemyStats stats = new EnemyStats();

    public int getScore = 10;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statsInd;

    private Rigidbody2D rb2D;

    public static bool enemyDamage = false;

    void Start()
    {
        stats.Init();
        if (statsInd != null)
        {
            statsInd.SetHealth(stats.currentHealth, stats.maxHealth);
        }
        enemySprite = GetComponent<SpriteRenderer>();
        sfxMan = FindObjectOfType<SFXManager>();
        //explosionRef = Resources.Load("EnemyExplode");
        rb2D = GetComponent<Rigidbody2D>();
        getScore = Random.Range(1, 100);
    }

    public void takeDamage(int damage)
    {
        anim.SetTrigger("Hurt");
        stats.currentHealth -= damage;
        flashActive = true;
        enemyDamage = true;
        flashCounter = flashLength;
        sfxMan.femaleFighterHurt.Play();

        if (stats.currentHealth <= 0)
        {
            sfxMan.FemaleFighterDead.Play();
            GetComponent<Animator>().SetBool("isDead", true);
            GameMaster.FemaleEnemyDefeat(this);
            enemyDamage = false;
        }


        if (statsInd != null)
        {
            statsInd.SetHealth(stats.currentHealth, stats.maxHealth);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.collider.GetComponent<Player>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
        }
    }

    void Update()
    {
        if (flashActive)
        {

            if (flashCounter > flashLength * 0.66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else if (flashCounter > flashLength * 0.33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > 0f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
                enemyDamage = false;
            }
            flashCounter -= Time.deltaTime;
        }

    }
}
