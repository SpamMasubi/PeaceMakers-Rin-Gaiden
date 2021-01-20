using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startCutscene : MonoBehaviour
{
    public static bool beginCutscene;
    public string loadScene;
    public float startDelay = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Begin Cutscene");
        beginCutscene = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CutsceneDialogue.isDialogueDone)
        {
            Debug.Log("End Cutscene");
            beginCutscene = false;
            StartCoroutine(nextScene());
        }
    }

    public IEnumerator nextScene()
    {
        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene(loadScene);
    }
}
