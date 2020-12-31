using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{

    public float speed = 2.5f;
    public float attackRange = 3f;
    public float specialAttackRange = 4f;
    float nextTimeToSearch = 0;

    Transform player;
    Rigidbody2D rb;

    Boss boss;
    Commander commander;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss.startBoss && !Commander.startCommanderBoss)
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
        }else if (Commander.startCommanderBoss && !Boss.startBoss)
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

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss.startBoss && !Commander.startCommanderBoss)
        {
            boss.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (Vector2.Distance(player.position, rb.position) <= attackRange)
                {
                    animator.SetTrigger("Attack");
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
        } else if (Commander.startCommanderBoss && !Boss.startBoss)
        {
            commander.LookAtPlayer();
            if (player != null)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                if (!CommanderHealth.isBossDead && !CommanderHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= attackRange)
                {
                    animator.SetTrigger("Attack");
                }else if(!CommanderHealth.isBossDead && CommanderHealth.isEnrage && Vector2.Distance(player.position, rb.position) <= specialAttackRange)
                {
                    animator.SetTrigger("Special Attack");
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
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Special Attack");
    }


}
