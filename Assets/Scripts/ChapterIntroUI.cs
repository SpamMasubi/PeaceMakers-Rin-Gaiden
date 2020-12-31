using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterIntroUI : MonoBehaviour
{
    private string levelToLoad;
    public GameObject chapterIntroUI;
    [SerializeField]
    private Text chapterTitle;
    [SerializeField]
    private Text chapterName;

    public GameObject startMessage;
    private bool canStart = false;
    public float startDelay = 5.0f;

    private SFXManager sfxMan;
    // Start is called before the first frame update
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();
        canStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelSelectorManager.isLevel1)
        {
            MusicController.musicCanPlay = false;
            chapterTitle.text = "Chapter 1";
            chapterName.text = "新しいの救世主伝説\n" + "A New Savior's Legend";
            StartCoroutine(activeStart());
            if (Input.GetKeyDown("space") && LevelSelectorManager.isLevel1 && canStart)
            {
                sfxMan.playerStart.Play();
                levelToLoad = LevelSelectorManager.levelID;
                SceneManager.LoadScene(levelToLoad);
                LevelSelectorManager.isLevel1 = false;
                MusicController.musicCanPlay = true;
                startMessage.SetActive(false);
                canStart = false;
            }
        }
        else if (LevelSelectorManager.isLevel2)
        {
            MusicController.musicCanPlay = false;
            chapterTitle.text = "Chapter 2";
            chapterName.text = "リンの戦い\n" + "Rin's Battle";
            StartCoroutine(activeStart());
            if (Input.GetKeyDown("space") && LevelSelectorManager.isLevel2 && canStart)
            {
                sfxMan.playerStart.Play();
                levelToLoad = LevelSelectorManager.levelID;
                SceneManager.LoadScene(levelToLoad);
                LevelSelectorManager.isLevel2 = false;
                MusicController.musicCanPlay = true;
                canStart = false;
            }
        }
    }

    public IEnumerator activeStart()
    {
        yield return new WaitForSeconds(startDelay);
        startMessage.SetActive(true);
        canStart = true;
    }
}
