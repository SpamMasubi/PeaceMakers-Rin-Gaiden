using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    //public float startDelay = 3.0f;

    public void Update()
    {
        if (BossHealth.isBossDead && !DialogueManager.isDialogueDone || CommanderHealth.isBossDead && !DialogueManager.isDialogueDone || 
            TsukimiHealth.isBossDead && !DialogueManager.isDialogueDone || XelciorHealth.isBossDead && !DialogueManager.isDialogueDone ||
            HannaHealth.isBossDead && !DialogueManager.isDialogueDone || ManaHealth.isBossDead && !DialogueManager.isDialogueDone 
            || IntroDialogueStart.playerInTrigger && !DialogueManager.isDialogueDone)
        {
            if (Input.GetKeyDown("space"))
            {
                nextDialogue();
            }
        }

        if (startCutscene.beginCutscene && !CutsceneDialogue.isDialogueDone)
        {
            if (Input.GetKeyDown("space"))
            {
                nextCutsceneDialogue();
            }
        }

        
        if (startEndCutscene.beginEndCutscene && !EndCutscene.isDialogueDone)
        {
            if (Input.GetKeyDown("space"))
            {
                nextEndCutsceneDialogue();
            }
        }

    }
    public void FixedUpdate()
    {
        if (BossHealth.isBossDead && !DialogueManager.isDialogueDone || CommanderHealth.isBossDead && !DialogueManager.isDialogueDone || 
            TsukimiHealth.isBossDead && !DialogueManager.isDialogueDone || XelciorHealth.isBossDead && !DialogueManager.isDialogueDone || 
            HannaHealth.isBossDead && !DialogueManager.isDialogueDone || ManaHealth.isBossDead && !DialogueManager.isDialogueDone || 
            IntroDialogueStart.playerInTrigger && !DialogueManager.isDialogueDone)
        {
            TriggerDialogue();
        }

        if (startCutscene.beginCutscene && !CutsceneDialogue.isDialogueDone)
        {
            openCutscene();
        }

        if (startEndCutscene.beginEndCutscene && !EndCutscene.isDialogueDone)
        {
            openEndCutscene();
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void nextDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentences();
    }

    public void openCutscene()
    {
        FindObjectOfType<CutsceneDialogue>().StartDialogue(dialogue);
    }

    public void nextCutsceneDialogue()
    {
        FindObjectOfType<CutsceneDialogue>().DisplayNextSentences();
    }

    public void openEndCutscene()
    {
        FindObjectOfType<EndCutscene>().StartDialogue(dialogue);
    }

    public void nextEndCutsceneDialogue()
    {
        FindObjectOfType<EndCutscene>().DisplayNextSentences();
    }
}
