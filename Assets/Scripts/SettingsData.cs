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



    public GameObject mobileInputHud;

    public bool toggleInput = false;
    public TMP_Text inputTypeText;

    public bool GameInputs
    {
        get { return toggleInput; }
        set
        {
            toggleInput = value;

            //ToggleBtn.gameObject.transform.GetComponentInChildren<TMP_Text>().text = toggleInput ? "JoyStick" : "KeyBoard";
            inputTypeText.text = toggleInput ? "JoyStick" : "KeyBoard";
            mobileInputHud.SetActive(toggleInput);
            //PlayerPrefs.SetInt(PlayerPrefsRegister.gameinputkey, toggleInput ? 1 : 0);
            ToggleControllerType(toggleInput);
        }
    }
    private void Start()
    {
        Initialization();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void Initialization()
    {

        /*we are using Scriptable Object to save Settings Data in it.
         and retreiving also*/
        //GameInputs =GameManager.sharedInstance.gameDataScript.isKeyboard;
        //MusicInfo =GameManager.sharedInstance.gameDataScript.musicStatus;

        //SoundInfo = GameManager.sharedInstance.gameDataScript.soundStatus;

        musicVolSlider.value = PlayerPrefs.GetFloat(PlayerPrefsRegister.musicVolume);
        SetMusicVolume(musicVolSlider.value);
        soundVolSlider.value = PlayerPrefs.GetFloat(PlayerPrefsRegister.soundVolume);
        SetMainSoundVol(soundVolSlider.value);
        //=================================================================//
        int inputType = PlayerPrefs.GetInt(PlayerPrefsRegister.gameinputkey/*, GameInputs ? 1 : 0*/);
        GameInputs = inputType == 1 ? true : false;
        int s = PlayerPrefs.GetInt(PlayerPrefsRegister.musickey);
        MusicInfo = s == 1 ? true : false;
        int sound = PlayerPrefs.GetInt(PlayerPrefsRegister.soundkey);
        SoundInfo = sound == 1 ? true : false;
    }
    #region Sound Button
    public void SoundBtn()
    {
        SoundInfo = !SoundInfo;
        PlayerPrefs.SetInt(PlayerPrefsRegister.soundkey, soundStatus ? 1 : 0);
        //GameManager.sharedInstance.gameDataScript.soundStatus = SoundInfo;
        if (SoundInfo)
            ClickSound("button");
    }
    #endregion
    #region Music Button
    public void MusicBtn()
    {
        MusicInfo = !MusicInfo;
        PlayerPrefs.SetInt(PlayerPrefsRegister.musickey, musicStatus ? 1 : 0);
        //GameManager.sharedInstance.gameDataScript.musicStatus = MusicInfo;
        MusicPlayPause(musicStatus);
        ClickSound("button");
    }
    #endregion
    public void ToggleControllerType(bool inputToggle)
    {
        //if (GameManager.sharedInstance.gameDataScript.isKeyboard)
        //{
        //PlayerPrefs.GetInt(gameinputkey, inputToggle ? 1 : 0);
        GameManager.sharedInstance.ToggleControllerType(inputToggle);
        //}
    }
    public void ControllerToggle()
    {
        GameInputs = !GameInputs;
        PlayerPrefs.SetInt(PlayerPrefsRegister.gameinputkey, GameInputs ? 1 : 0);
        //GameManager.sharedInstance.gameDataScript.isKeyboard = GameInputs;
        GameManager.sharedInstance.ToggleControllerType(GameInputs);
    }

    #region SoundMain
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


    void ClickSound(string _soundname)
    {
        if (SoundInfo)
            GameManager.sharedInstance.SoundPlay(_soundname);
    }
    public void MainSoundVolSlider(float _vol)
    {
        //GameManager.sharedInstance.gameDataScript.soundVolume = _vol;
        PlayerPrefs.SetFloat(PlayerPrefsRegister.soundVolume, _vol);
        SetMainSoundVol(_vol);
    }
    public void SetMainSoundVol(float vol)
    {
        GameManager.sharedInstance.GMainSoundVolume(vol);
    }
    #endregion

    #region MusicOnly
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

    public void MusicPlayPause(bool _play)
    {
        GameManager.sharedInstance.MusicStatus(_play);
    }

    public void MusicVolumeSlider(float _volume)
    {
        PlayerPrefs.SetFloat(PlayerPrefsRegister.musicVolume, _volume);
        //GameManager.sharedInstance.gameDataScript.musicVolume = _volume;
        SetMusicVolume(_volume);
    }
    public void SetMusicVolume(float _volume)
    {
        GameManager.sharedInstance.GSetMusicVolume(_volume);
    }

    #endregion
    public void Resetall()
    {
        //GameManager.sharedInstance.gameDataScript.ResetSettings();
        PlayerPrefs.DeleteAll();
        Initialization(); //after deleting all prefes, we must set default values to all prefs
    }
    //#endregion


}//class




