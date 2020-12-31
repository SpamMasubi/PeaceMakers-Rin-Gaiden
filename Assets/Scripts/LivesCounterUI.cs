using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounterUI : MonoBehaviour
{

    private Text LivesText;
    // Start is called before the first frame update
    void Awake()
    {
        LivesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        LivesText.text = "x" + GameMaster.playerLives.ToString();
    }
}
