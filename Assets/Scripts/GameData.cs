using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "Create New GameData")]
public class GameData : ScriptableObject
{
    public string userName = "";

    public bool soundStatus = true;
    public bool musicStatus = true;
    public bool isKeyboard = true;

    [Range(0f, 1f)]
    public float musicVolume ;
    [Range(0f, 1f)]
    public float soundVolume;

    public TimeSpan SavetimeSpan;
    public void ResetSettings()
    {
        soundStatus = true;
        musicStatus = true;
        isKeyboard = true;
        musicVolume = 1f;
        soundVolume = 1f;
    }
}
