using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGBandit : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float agroRange;

    Rigidbody2D rb2d;

    public GameObject enemyBullet;

    public Transform launchPoint;

    public float waitBetweenShots;
    private float shotCounter;

    Animator anim;

    float nextTimeToSearch = 0;
    Vector3 lastTargetPosition;
    private SFXManager sfxMan;

    // Use this for initialization
    void Start()
    {
        shotCounter = waitBetweenShots;
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        lastTargetPosition = player.position;
        sfxMan = FindObjectOfType<SFXManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroRange)
        {
            //shoot player
            ShootPlayer();
        }
        else
        {
            //stop shooting player
            StopShootingPlayer();
        }


        /*Debug.DrawLine(new Vector3(transform.position.x - playerRange, transform.position.y, transform.position.z), new Vector3(transform.position.x + playerRange, transform.position.y, transform.position.z));
        shotCounter -= Time.deltaTime;
        if (transform.localScale.x > 0 && player.transform.position.x < transform.position.x && player.transform.position.x < transform.position.x - playerRange && shotCounter < 0)
        {
            
            GameObject arrow = (GameObject)Instantiate(enemyBullet, launchPoint.position, launchPoint.rotation);
            arrow.SetActive(true);
            shotCounter = waitBetweenShots;
        }*/
    }

    void StopShootingPlayer()
    {
        rb2d.velocity = Vector2.zero;
        anim.Play("MGBanditIdle");
    }

    void ShootPlayer()
    {
        shotCounter -= Time.deltaTime;
        if (transform.position.x > player.position.x && shotCounter < 0)
        {
            //enemy to the left side of the player, shoot left
            //rb2d.velocity = new Vector2(-moveSpeed, 0);

            anim.Play("MGBanditAttack");
            sfxMan.gunShotMultiple.Play();
            GameObject projectile = (GameObject)Instantiate(enemyBullet, launchPoint.position, launchPoint.rotation);
            projectile.SetActive(true);
            shotCounter = waitBetweenShots;
        }
        else if (transform.position.x == player.position.x || transform.position.x < player.position.x)
        {
            rb2d.velocity = Vector2.zero;
            anim.Play("MGBanditIdle");
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
