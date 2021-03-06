using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startEndCutscene : MonoBehaviour
{
    public static bool beginEndCutscene;
    public string loadScene;
    public float startDelay = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        beginEndCutscene = true;
        MusicController.musicCanPlay = true;
        XelciorHealth.lvlComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EndCutscene.isDialogueDone)
        {
            beginEndCutscene = false;
            MainMenu.newGame = false;
            StartCoroutine(nextScene());
        }
    }

    public IEnumerator nextScene()
    {
        yield return new WaitForSeconds(startDelay);
        //MusicController.musicCanPlay = false;
        SceneManager.LoadScene(loadScene);
        MusicController.musicCanPlay = false;
    }
}
