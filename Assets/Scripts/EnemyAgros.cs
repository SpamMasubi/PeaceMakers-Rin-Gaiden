using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgros : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Animator anim;

    Rigidbody2D rb2d;

    bool isFacingLeft;

    private bool isAgros;
    private bool isSearching;
    private bool isAttacking;
    float nextTimeToSearch = 0;
    Vector3 lastTargetPosition;

    public float waitBetweenAttacks;
    private float attackCounter;
    private float distToPlayer;

    private SFXManager sfxMan;

    // Start is called before the first frame update
    void Start()
    {
        attackCounter = waitBetweenAttacks;

        rb2d = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        lastTargetPosition = player.position;

        sfxMan = FindObjectOfType<SFXManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            distToPlayer = Vector2.Distance(transform.position, player.position);
            //print("player distance: " + distToPlayer);
        }
        if (player == null)
        {
            StopChasingPlayer();
            FindPlayer();
            return;
        }

        if (CanSeePlayer(agroRange))
        {
            //agro player
            isAgros = true;
            
            if (distToPlayer <= 4)
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }
            
        }else
        {
            if (isAgros)
                {
                    if (!isSearching)
                    {
                        isSearching = true;
                        Invoke("StopChasingPlayer", 3);
                    }
                }
        }

        if (isAgros && isAttacking)
        {
            attackPlayer();
        }else if (isAgros && !isAttacking) {
            ChasePlayer();
        }

        

        
        /*
        //distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if(distToPlayer < agroRange)
        {
            //chase player
            ChasePlayer();
        }
        else
        {
            //stop chasing
            StopChasingPlayer();
        }*/
    }

    bool CanSeePlayer(float distance)
    {
        bool value = false;
        var castDist = -distance;

        if (isFacingLeft)
        {
            castDist = distance;
        }

        Vector2 endPoint = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPoint, 1 << LayerMask.NameToLayer("Player"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                value = true;
            }
            else
            {
                value = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPoint, Color.blue);
        }

        return value;
    }

    void StopChasingPlayer()
    {
        isAgros = false;
        isSearching = false;
        isAttacking = false;
        rb2d.velocity = Vector2.zero;
        anim.Play("BanditBIdle");
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy to the left side of the player, moves left
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        } else if(transform.position.x == player.position.x)
        {
            rb2d.velocity = Vector2.zero;
            anim.Play("BanditBIdle");
            isFacingLeft = false; 
        }
        else
        {
            //enemy to the right side of the player, moves right
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
            isFacingLeft = false;
        }

        anim.Play("BanditBRunning");
    }

    void attackPlayer()
    {
        attackCounter -= Time.deltaTime;
        if (distToPlayer <= 4 && attackCounter < 0)
        {
            rb2d.velocity = Vector2.zero;
            anim.Play("BanditBAttack");
            sfxMan.weaponSwing.Play();
            attackCounter = waitBetweenAttacks;
        }
        isAttacking = false;
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
