using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enrage : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss.startBoss)
        {
            animator.GetComponent<BossHealth>().isInvulnerable = true;
        }else if (Commander.startCommanderBoss)
        {
            animator.GetComponent<CommanderHealth>().isInvulnerable = true;
        }
        else if (Tsukimi.startTsukimiBoss)
        {
            animator.GetComponent<TsukimiHealth>().isInvulnerable = true;
        }
        else if (Xelcior.startXelciorBoss)
        {
            animator.GetComponent<XelciorHealth>().isInvulnerable = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss.startBoss)
        {
            animator.GetComponent<BossHealth>().isInvulnerable = false;
        }
        else if (Commander.startCommanderBoss)
        {
            animator.GetComponent<CommanderHealth>().isInvulnerable = false;
        }else if (Tsukimi.startTsukimiBoss)
        {
            animator.GetComponent<TsukimiHealth>().isInvulnerable = false;
        }
        else if (Xelcior.startXelciorBoss)
        {
            animator.GetComponent<XelciorHealth>().isInvulnerable = false;
        }
    }
}
