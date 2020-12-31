using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            nextDialogue();
        }
    }
    public void FixedUpdate()
    {
        if (BossHealth.isBossDead && !DialogueManager.isDialogueDone || CommanderHealth.isBossDead && !DialogueManager.isDialogueDone)
        {
            TriggerDialogue();
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
}
