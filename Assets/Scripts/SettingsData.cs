using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsData : MonoBehaviour
{
    public Slider musicVolSlider;
    public Slider soundVolSlider;
    public bool musicStatus = true;
    public bool soundStatus = true;

    public TMP_Text musictext;
    public Text soundtext;

    string musickey = "music";
    string soundkey = "sound";
    string volumekey = "volume";

    public GameObject joyStickObj;
    string gameinputkey = "inputKey";
    bool toggleInput;
    public TMP_Text inputTypeText;
   
    public bool GameInputs
    {
        get { return toggleInput; }
        set
        {
            toggleInput = value;

            //ToggleBtn.gameObject.transform.GetComponentInChildren<TMP_Text>().text = toggleInput ? "JoyStick" : "KeyBoard";
            inputTypeText.text = toggleInput ? "JoyStick" : "KeyBoard";
            joyStickObj.SetActive(toggleInput);
            //PlayerPrefs.SetInt(gameinputkey, toggleInput ? 1 : 0);
            ToggleControllerType(toggleInput);
        }
    }
    private void Start()
    {
        Initialization();
    }

    public void Initialization()
    {

        /*we are using Scriptable Object to save Settings Data in it.
         and retreiving also*/
        GameInputs =GameManager.sharedInstance.gameDataScript.isKeyboard;
        MusicInfo =GameManager.sharedInstance.gameDataScript.musicStatus;

        SoundInfo = GameManager.sharedInstance.gameDataScript.soundStatus;

        musicVolSlider.value = GameManager.sharedInstance.gameDataScript.musicVolume;
        SetMusicVolume(musicVolSlider.value);
        soundVolSlider.value = GameManager.sharedInstance.gameDataScript.soundVolume;
        SetMainSoundVol(soundVolSlider.value);
      //int inputType=  PlayerPrefs.GetInt(gameinputkey,GameInputs ? 1 : 0);
        //GameInputs = inputType == 1 ? true : false;
        //int s = PlayerPrefs.GetInt(musickey);
        //MusicInfo = s == 1 ? true : false;
        //int sound = PlayerPrefs.GetInt(soundkey);
        //SoundInfo = sound == 1 ? true : false;
        //volume = PlayerPrefs.GetFloat(volumekey);
        //gameIsOn = true;
    }
    public void ToggleControllerType(bool inputToggle)
    {
        GameManager.sharedInstance.ToggleControllerType(inputToggle);
    }
    public void ControllerToggle()
    {
        GameInputs = !GameInputs;
        //PlayerPrefs.GetInt(gameinputkey, GameInputs ? 1 : 0);
        GameManager.sharedInstance.gameDataScript.isKeyboard = GameInputs;
    }

    #region Sound
    public bool SoundInfo
    {
        get
        {
            return soundStatus;
        }
        set
        {
            soundStatus = value;
            soundtext.text = soundStatus ? "Sound : On" : "Sound : Off";
            GameManager.sharedInstance.MusicStatus(soundStatus);
        }
    }

    public void SoundBtn()
    {
        SoundInfo = !SoundInfo;
        //PlayerPrefs.SetInt(soundkey, soundStatus ? 1 : 0);
        GameManager.sharedInstance.gameDataScript.soundStatus = SoundInfo;
        if(SoundInfo)
        ClickSound("button");
    }
    void ClickSound(string _soundname)
    {
        if(SoundInfo)
        GameManager.sharedInstance.SoundPlay(_soundname);
    }
    #endregion

    #region Music
    public bool MusicInfo
    {
        get
        {
            return musicStatus;
        }
        set
        {
            musicStatus = value;
            musictext.text = musicStatus ? "Music : ON" : "Music : Off";
        }
    }
    public void MusicBtn()
    {
        MusicInfo = !MusicInfo;
        //PlayerPrefs.SetInt(musickey, musicStatus ? 1 : 0);
        GameManager.sharedInstance.gameDataScript.musicStatus = MusicInfo;
        MusicPlayPause(musicStatus);
        ClickSound("button");
    }
    public void MusicPlayPause(bool _play)
    {
        GameManager.sharedInstance.MusicStatus(_play);
    }
    public void MusicVolumeSlider(float _volume)
    {
        //PlayerPrefs.SetFloat(volumekey, volume);
        GameManager.sharedInstance.gameDataScript.musicVolume = _volume;
        SetMusicVolume(_volume);
    }
    public void SetMusicVolume(float _volume)
    {
        GameManager.sharedInstance.GSetMusicVolume(_volume);
    }
    public void SetMainSoundVol(float vol)
    {
        GameManager.sharedInstance.GMainSoundVolume(vol);
    }
    public void MainSoundVolSlider(float _vol)
    {
        GameManager.sharedInstance.gameDataScript.soundVolume = _vol;
        SetMainSoundVol(_vol);
    }
    #endregion
    public void Resetall()
    {
        GameManager.sharedInstance.gameDataScript.ResetSettings();
        //PlayerPrefs.DeleteAll();
        Initialization(); //after deleting all prefes, we must set default values to all prefs
    }
    //#endregion
}//class

    //#region Playerdata
    //string playerNameKey = "playername";
    //string playername;
    //public TMP_InputField playernameText;
    //public TMP_Text showplayername;

    //void PlayerNameInit()
    //{
    //    if (PlayerPrefs.HasKey(playerNameKey))
    //    {

    //        if (GameManager.sharedInstance.GetCurrentSceneIndex() == 0)
    //        {
    //            GameManager.sharedInstance.SwitchPanel(EUI_PANEL.mainMenuE);
    //            //ActivatePanel(EUI_PANEL.mainMenuE);
    //        }

    //        showplayername.text = "Welcome " + PlayerPrefs.GetString(playerNameKey, playername);
    //    }
    //    else
    //    {
    //        GameManager.sharedInstance.SwitchPanel(EUI_PANEL.welcomeE);
    //        //ActivatePanel(EUI_PANEL.welcomeE);
    //    }
    //}


    //public void EnterPlayerName(string _name)
    //{
    //    playername = _name;
    //    playernameText.text = playername;
    //    PlayerPrefs.SetString(playerNameKey, playername);
    //}
