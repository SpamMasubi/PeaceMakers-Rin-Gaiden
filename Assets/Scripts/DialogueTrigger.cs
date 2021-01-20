using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Update()
    {
        if (BossHealth.isBossDead && !DialogueManager.isDialogueDone || CommanderHealth.isBossDead && !DialogueManager.isDialogueDone)
        {
            if (Input.GetKeyDown("space"))
            {
                nextDialogue();
            }
        }

        if (startCutscene.beginCutscene && !DialogueManager.isDialogueDone)
        {
            if (Input.GetKeyDown("space"))
            {
                nextCutsceneDialogue();
            }
        }

    }
    public void FixedUpdate()
    {
        if (BossHealth.isBossDead && !DialogueManager.isDialogueDone || CommanderHealth.isBossDead && !DialogueManager.isDialogueDone)
        {
            TriggerDialogue();
        }

        if (startCutscene.beginCutscene && !DialogueManager.isDialogueDone)
        {
            openCutscene();
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
}
