using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("Optional: ")]
    public string playerName;

    [TextArea(3,1000)]
    public string[] sentences;
}
