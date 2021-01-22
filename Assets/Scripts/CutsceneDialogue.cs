using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDialogue : MonoBehaviour
{
    public Text dialogueText;
    private SFXManager sfxMan;

    public GameObject[] cutscenes;

    public static bool isDialogueDone = false;

    public static Queue<string> sentences;
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

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();

    }

    public void DisplayNextSentences()
    {
        if (startCutscene.beginCutscene)
        {
            if (sentences.Count == 12)
            {
                cutscenes[0].SetActive(true);
            } else if(sentences.Count == 11)
            {
                cutscenes[0].SetActive(false);
                cutscenes[1].SetActive(true);
            }else if (sentences.Count == 9)
            {
                cutscenes[1].SetActive(false);
                cutscenes[2].SetActive(true);
            }
            else if (sentences.Count == 8)
            {
                cutscenes[2].SetActive(false);
                cutscenes[3].SetActive(true);
            }
            else if (sentences.Count == 7)
            {
                cutscenes[3].SetActive(false);
                cutscenes[4].SetActive(true);
            }
            else if (sentences.Count == 6)
            {
                cutscenes[4].SetActive(false);
                cutscenes[5].SetActive(true);
            }
            else if (sentences.Count == 4)
            {
                cutscenes[5].SetActive(false);
                cutscenes[6].SetActive(true);
            }

        }


        if (sentences.Count == 0)
        {
            EndDialogue();
            cutscenes[6].SetActive(false);
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
        isDialogueDone = true;
        Time.timeScale = 1;
    }
}
