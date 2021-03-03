using System;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New GameData", menuName = "Create New GameData")]
public class PlayerPrefsRegister : /*ScriptableObject,*/MonoBehaviour
{
    //public string userName = "";

    //public bool soundStatus = true;
    //public bool musicStatus = true;
    //public bool isKeyboard = true;
    //public static string volumekey = "volume";

  

    public static string musickey = "music";
    public static string soundkey = "sound";
    public static string gameinputkey = "inputKey";
    public static string playerNameKey = "playername";
    public static string musicVolume;
    public static string soundVolume;

    public static string savetimeSpan;
    //public void ResetSettings()
    //{
    //    soundStatus = true;
    //    musicStatus = true;
    //    isKeyboard = true;
    //    musicVolume = 1f;
    //    soundVolume = 1f;
    //}
}
