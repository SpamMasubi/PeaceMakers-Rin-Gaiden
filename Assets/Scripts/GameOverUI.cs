using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private SFXManager sfxMan;
    public string loadScene;
    public string loadMainMenu;

    public static bool returnToCheckPoint;
    public static bool playAgain = false;

    void Start()
    {
        MusicController.musicCanPlay = false;
        sfxMan = FindObjectOfType<SFXManager>();
    }

    public void Quit()
    {
        Debug.Log("Application Quit!");
        GameMaster.playGame = false;
        MusicController.musicCanPlay = true;
        GameMaster.isGameOver = false;
        GameMaster.charaDead = false;
        DialogueManager.isDialogueDone = false;
        Boss.startBoss = false;
        Commander.startCommanderBoss = false;
        Tsukimi.startTsukimiBoss = false;
        Xelcior.startXelciorBoss = false;
        Hanna.startHannaBoss = false;
        Mana.startManaBoss = false;
        if (MainMenu.newGame)
        {
            if (LevelSelectorManager.firstLevelComplete && !CommanderHealth.lvlComplete)
            {
                BossHealth.canUnlock = true;
            }
            else if (CommanderHealth.lvlComplete && !TsukimiHealth.lvlComplete)
            {
                CommanderHealth.canUnlock = true;
            }
            else if (TsukimiHealth.lvlComplete && !XelciorHealth.lvlComplete)
            {
                TsukimiHealth.canUnlock = true;
            }
        }
        sfxMan.selection.Play();
        SceneManager.LoadScene(loadMainMenu);
    }

    public void Retry()
    {
        Debug.Log("Let's play again");
        GameMaster.playGame = false;
        MusicController.musicCanPlay = true;
        GameMaster.isGameOver = false;
        GameMaster.charaDead = false;
        DialogueManager.isDialogueDone = false;
        if (MainMenu.newGame)
        {
            if (LevelSelectorManager.firstLevelComplete && !CommanderHealth.lvlComplete)
            {
                BossHealth.canUnlock = true;
            }
            else if (CommanderHealth.lvlComplete && !TsukimiHealth.lvlComplete)
            {
                CommanderHealth.canUnlock = true;
            }
            else if (TsukimiHealth.lvlComplete && !XelciorHealth.lvlComplete)
            {
                TsukimiHealth.canUnlock = true;
            }
        }

        sfxMan.selection.Play();
        sfxMan.playerRetry.Play();
        if (Boss.startBoss)
        {
            SceneManager.LoadScene("Level 1 Boss");
            playAgain = true;
        }
        else if (Commander.startCommanderBoss)
        {
            SceneManager.LoadScene("Level 2 Boss");
            playAgain = true;
        }
        else if (Tsukimi.startTsukimiBoss)
        {
            SceneManager.LoadScene("Level 3 Boss");
            playAgain = true;
        }
        else if (Xelcior.startXelciorBoss)
        {
            SceneManager.LoadScene("Level 4 Boss");
            playAgain = true;
        }
        else if (MainMenu.secretStory)
        {
            playAgain = true;
            if (Hanna.startHannaBoss && !Mana.startManaBoss)
            {
                SceneManager.LoadScene("Secret Story Boss");
            }
            else if (!Hanna.startHannaBoss && Mana.startManaBoss)
            {
                SceneManager.LoadScene("Secret Story Boss 2");
            }
            else
            {
                SceneManager.LoadScene("Secret Story Level");
            }
            
        }
        else
        {
            returnToCheckPoint = true;
            playAgain = true;
            if (LevelSelectorManager.isLevel1 && LevelLoadStage.checkpoint)
            {
                SceneManager.LoadScene("Level 1-2");
            }
            else if(LevelSelectorManager.isLevel2 && LevelLoadStage.checkpoint)
            {
                SceneManager.LoadScene("Level 2-2");
            }
            else if(LevelSelectorManager.isLevel3 && LevelLoadStage.checkpoint)
            {
                SceneManager.LoadScene("Level 3-2");
            }
            else if(LevelSelectorManager.isLevelFinal && LevelLoadStage.checkpoint)
            {
                SceneManager.LoadScene("Level 4-2");
            }
            else
            {
                SceneManager.LoadScene(loadScene);
            }

        }
    }
    public void OnMouseOver()
    {
        sfxMan.selectionHover.Play();
    }
}
