using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private SFXManager sfxMan;
    public Text nameText;
    public Text dialogueText;

    public Animator anim;

    public static bool isDialogueDone = false;

    private Queue<string> sentences;
    private float typingSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        isDialogueDone = false;
        sfxMan = FindObjectOfType<SFXManager>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0;
        anim.SetBool("isOpen", true);

        nameText.text = dialogue.playerName;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();

    }

    public void DisplayNextSentences()
    {
        if (BossHealth.isBossDead) {
            if (sentences.Count == 6 || sentences.Count == 5 || sentences.Count == 1)
            {

                nameText.text = "Bandit Leader: ";
            }
            else
            {
                nameText.text = "Rin: ";
            }
        }else if (CommanderHealth.isBossDead)
        {
            if (sentences.Count == 9 || sentences.Count == 8 || sentences.Count == 5 || sentences.Count == 4 || sentences.Count == 2 || sentences.Count == 1)
            {

                nameText.text = "Commander: ";
            }
            else
            {
                nameText.text = "Rin: ";
            }
        }
        else if (TsukimiHealth.isBossDead)
        {
            if (sentences.Count == 13 || sentences.Count == 12 || sentences.Count == 6 || sentences.Count == 5)
            {

                nameText.text = "Rin: ";
            }
            else
            {
                nameText.text = "Tsukimi: ";
            }
        }
        else if (XelciorHealth.isBossDead)
        {
            if (sentences.Count == 15 || sentences.Count == 14 || sentences.Count == 12 || sentences.Count == 8 || sentences.Count == 5 || sentences.Count == 4)
            {

                nameText.text = "Rin: ";
            }
            else
            {
                nameText.text = "Xelcior: ";
            }
        }
        else if (HannaHealth.isBossDead)
        {
            if (sentences.Count == 24  || sentences.Count == 23 || sentences.Count == 22 || sentences.Count == 20 
                || sentences.Count == 17 || sentences.Count == 12 || sentences.Count == 11 || sentences.Count == 6 
                || sentences.Count == 3 || sentences.Count == 2 || sentences.Count == 1)
            {

                nameText.text = "Rin: ";
            }
            else
            {
                nameText.text = "Hanna: ";
            }
        }
        else if (ManaHealth.isBossDead)
        {
            if (sentences.Count == 19 || sentences.Count == 17 || sentences.Count == 15 || sentences.Count == 14
                || sentences.Count == 13 || sentences.Count == 11 || sentences.Count == 9 || sentences.Count == 7
                || sentences.Count == 6 || sentences.Count == 4 || sentences.Count == 1)
            {

                nameText.text = "Rin: ";
            }
            else
            {
                nameText.text = "Mana: ";
            }
        }


        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            sfxMan.selectionHover.Play();
            yield return null;
            //yield return new WaitForSeconds(typingSpeed);
        }
        sfxMan.selectionHover.Stop();
    } 

    

    public void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        isDialogueDone = true;
        Time.timeScale = 1;
    }
}
