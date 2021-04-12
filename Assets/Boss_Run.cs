using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float attackRange = 3f;
    public float specialAttackRange = 4f;
    float nextTimeToSearch = 0f;
    float attackTimer = 0f;

    Transform player;
    Rigidbody2D rb;

    Boss boss;
    Commander commander;
    Tsukimi tsukimi;
    Xelcior xelcior;
    Hanna hanna;
    Mana mana;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss.startBoss)
        {
            if (player == null)
            {
                if (nextTimeToSearch <= Time.time)
                {
                    GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
                    if (searchPlayer != null)
                        player = searchPlayer.transform;
                    nextTimeToSearch = Time.time + 0.2f;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            rb = animator.GetComponent<Rigidbody2D>();
            boss = animator.GetComponent<Boss>();
        }else if (Commander.startCommanderBoss)
        {
            if (player == null)
            {
                if (nextTimeToSearch <= Time.time)
                {
                    GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
                    if (searchPlayer != null)
                        player = searchPlayer.transform;
                    nextTimeToSearch = Time.time + 0.2f;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            rb = animator.GetComponent<Rigidbody2D>();
            commander = animator.GetComponent<Commander>();
        }
        else if (Tsukimi.startTsukimiBoss)
        {
            if (player == null)
            {
                if (nextTimeToSearch <= Time.time)
                {
                    GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
                    if (searchPlayer != null)
                        player = searchPlayer.transform;
                    nextTimeToSearch = Time.time + 0.2f;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            rb = animator.GetComponent<Rigidbody2D>();
            tsukimi = animator.GetComponent<Tsukimi>();
        }
        else if (Xelcior.startXelciorBoss)
        {
            if (player == null)
            {
                if (nextTimeToSearch <= Time.time)
                {
                    GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
                    if (searchPlayer != null)
                        player = searchPlayer.transform;
                    nextTimeToSearch = Time.time + 0.2f;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            rb = animator.GetComponent<Rigidbody2D>();
            xelcior = animator.GetComponent<Xelcior>();
        } 
        else if(Hanna.startHannaBoss)
        {
            if (player == null)
            {
                if (nextTimeToSearch <= Time.time)
                {
                    GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
                    if (searchPlayer != null)
                        player = searchPlayer.transform;
                    nextTimeToSearch = Time.time + 0.2f;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            rb = animator.GetComponent<Rigidbody2D>();
            hanna = animator.GetComponent<Hanna>();
        }
        else if (Mana.startManaBoss)
        {
            if (player == null)
            {
                if (nextTimeToSearch <= Time.time)
                {
                    GameObject searchPlayer = GameObject.FindGameObjectWithTag("Player");
                    if (searchPlayer != null)
                        player = searchPlayer.transform;
                    nextTimeToSearch = Time.time + 0.2f;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            rb = animator.GetComponent<Rigidbody2D>();
            mana = animator.GetComponent<Mana>();
        }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss.startBoss)
        {
            boss.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);
                if (attackTimer <= 0f)
                {
                    attackTimer = 1.5f;
                }
                if (attackTimer > 0f)
                {
                    attackTimer -= Time.deltaTime;
                    if (Vector2.Distance(player.position, rb.position) <= attackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Attack");
                    }
                }
            }
            else
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
        else if (Commander.startCommanderBoss)
        {
            commander.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (attackTimer <= 0f)
                {
                    attackTimer = 1.5f;
                }
                if (attackTimer > 0f)
                {
                    attackTimer -= Time.deltaTime;
                    if (!CommanderHealth.isBossDead && !CommanderHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= attackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Attack");
                    }
                    else if (!CommanderHealth.isBossDead && CommanderHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= specialAttackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Special Attack");
                    }
                }
            }
            else
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
        else if (Tsukimi.startTsukimiBoss)
        {
            tsukimi.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (attackTimer <= 0f)
                {
                    attackTimer = 1.5f;
                }
                if (attackTimer > 0f)
                {
                    attackTimer -= Time.deltaTime;
                    if (!TsukimiHealth.isBossDead && !TsukimiHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= attackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Attack");
                    }
                    else if (!TsukimiHealth.isBossDead && TsukimiHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= specialAttackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Special Attack");
                    }
                }
            }
            else
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
        else if (Xelcior.startXelciorBoss)
        {
            xelcior.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (attackTimer <= 0f)
                {
                    attackTimer = 1.5f;
                }
                if (attackTimer > 0f)
                {
                    attackTimer -= Time.deltaTime;
                    if (!XelciorHealth.isBossDead && !XelciorHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= attackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Attack");
                    }
                    else if (!XelciorHealth.isBossDead && XelciorHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= specialAttackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Special Attack");
                    }
                }
            }
            else
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
        else if (Hanna.startHannaBoss)
        {
            hanna.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (attackTimer <= 0f)
                {
                    attackTimer = 1.5f;
                }
                if (attackTimer > 0f)
                {
                    attackTimer -= Time.deltaTime;
                    if (!HannaHealth.isBossDead && !HannaHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= attackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Attack");
                    }
                    else if (!HannaHealth.isBossDead && HannaHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= specialAttackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Special Attack");
                    }
                }
            }
            else
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
        else if (Mana.startManaBoss)
        {
            mana.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (attackTimer <= 0f)
                {
                    attackTimer = 1.5f;
                }
                if (attackTimer > 0f)
                {
                    attackTimer -= Time.deltaTime;
                    if (!ManaHealth.isBossDead && !ManaHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= attackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Attack");
                    }
                    else if (!ManaHealth.isBossDead && ManaHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= specialAttackRange && attackTimer <= 0f)
                    {
                        animator.SetTrigger("Special Attack");
                    }
                }
            }
            else
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
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss.startBoss)
        {
            animator.ResetTrigger("Attack");

        } else if (Commander.startCommanderBoss || Tsukimi.startTsukimiBoss || Xelcior.startXelciorBoss || Hanna.startHannaBoss || Mana.startManaBoss) {
            animator.ResetTrigger("Attack");
            animator.ResetTrigger("Special Attack");
        }
    }


}
