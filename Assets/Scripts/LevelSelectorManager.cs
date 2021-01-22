﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField]
    GameObject Selector;

    [SerializeField]
    GameObject[] row1;

    [SerializeField]
    GameObject[] row2;

    const int cols = 2;

    const int rows = 2;

    Vector2 posIndex;
    GameObject curSlot;

    public static bool isLevel1 = false;
    public static bool isLevel2 = false;
    public static bool isLevel3 = false;
    public static bool isLevelFinal = false;

    public static string levelID;
    public string loadLevel;

    bool isMoving = false;
    //declare 2D grid
    public GameObject[,] grid = new GameObject[cols, rows];

    private SFXManager sfxMan;
    // Start is called before the first frame update
    void Start()
    {
        AddRowtoGrid(0, row1);
        AddRowtoGrid(1, row2);

        posIndex = new Vector2(0, 0);
        curSlot = grid[0, 0];

        isLevel1 = false;
        isLevel2 = false;
        isLevel3 = false;
        isLevelFinal = false;

        sfxMan = FindObjectOfType<SFXManager>();
    }

    void AddRowtoGrid(int index, GameObject[] row)
    {
        for (int i = 0; i < 2; i++)
        {
            grid[index, i] = row[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        if(xAxis > 0)
        {
            //input right
            moveSelector("right");

        }
        else if(xAxis < 0)
        {
            //input left
            moveSelector("left");
        }
        else if(yAxis > 0)
        {
            //input up
            moveSelector("up");
        }
        else if(yAxis < 0)
        {
            //input down
            moveSelector("down");
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
           
            levelID = curSlot.GetComponent<LevelSelectItems>().levelID;

            sfxMan.selection.Play();
            if (levelID == "Level 1")
            {
                sfxMan.banditVoice.Play();
                SceneManager.LoadScene(loadLevel);
                isLevel1 = true;
            }else if(levelID == "Level 2")
            {
                sfxMan.Level2BossVoice.Play();
                SceneManager.LoadScene(loadLevel);
                isLevel2 = true;
            }
        }
    }

    void moveSelector(string direction)
    {
        if (!isMoving)
        {
            isMoving = true;

            if (direction == "right")
            {
                if (posIndex.x < cols - 1)
                {
                    sfxMan.selectionHover.Play();
                    posIndex.x += 1;
                }
            }else if(direction == "left")
            {
                if(posIndex.x > 0)
                {
                    sfxMan.selectionHover.Play();
                    posIndex.x -= 1;
                }
            }else if(direction == "up")
            {
                if(posIndex.y > 0)
                {
                    sfxMan.selectionHover.Play();
                    posIndex.y -= 1;
                }
            }
            else if(direction == "down")
            {
                if(posIndex.y < rows - 1)
                {
                    sfxMan.selectionHover.Play();
                    posIndex.y += 1;
                }
            }
            curSlot = grid[(int)posIndex.y, (int)posIndex.x];
            Selector.transform.position = curSlot.transform.position;

            Invoke("ResetMoving", 0.2f);
        }
    }

    void ResetMoving()
    {
        isMoving = false;
    }
}