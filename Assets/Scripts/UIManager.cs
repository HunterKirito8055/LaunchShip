using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
  
  // public UIPanel[] allpanels;
    public List<UIPanel> allpanelsList;
    public EUI_PANEL startingPanel;

    [SerializeField]
    private Gradient healthGradient;
    [SerializeField]
    private Slider healthSilder;
    private Image healthFillBar;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject  backGroundImage;
    //[SerializeField]
    //private GameObject mainMenuPanel, settingsPanel,pausePanel,inputField_Panel, hudPanel, youWonPanel, gameOverPanel;


    public TMP_Text powerUpname,powerUpTimerText,displayLevelText;
    public Image powerUpImage;
    public Button previousBtn,nextLevelBtn;
    bool gameIsOn;
    
    public int currentLevelIndex;
    bool isPauseGame;
    public bool IsPauseGame{
        get
        {
            return isPauseGame;
        }
        set
        {
            isPauseGame = value;
            if (isPauseGame)
            {
                ActivatePanel(EUI_PANEL.pauseE);
                Time.timeScale = 0f;
            }
            else
            { 
                ActivatePanel(EUI_PANEL.hudE);
                Time.timeScale = 1f;
            }
        }
  }
  

    private void Awake()
    {
        // allpanels = GetComponentsInChildren<UIPanel>();
        allpanelsList = new List<UIPanel>();
        for(int i = 1; i < transform.childCount; i++)
        {
            allpanelsList.Add(transform.GetChild(i).GetComponent<UIPanel>());
        }
        healthFillBar = healthSilder.fillRect.GetComponent<Image>();
        
    }
    private void Start()
    {
        Initialise();
        LoadButtonSetup();
        currentLevelIndex = GetCurrentSceneIndex();
        DisplayLevel(currentLevelIndex);
        if(PlayerPrefs.HasKey(playerNameKey))
        {
            if(currentLevelIndex<1)
            {
                //ActivatePanel(EUI_PANEL.mainMenuE);
                ActivatePanel(startingPanel);
                playernameText.text = "Welcome " + PlayerPrefs.GetString(playerNameKey);
            }
            else
            {
                StartGame();
            }
        }
        else
        {
            ActivatePanel(EUI_PANEL.welcomeE);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !gameIsOn)
        {
            IsPauseGame = !IsPauseGame;
            print(IsPauseGame);
        }
    }
    #region Panels

    public void ActivatePanel(UIPanel _uiPanel)
    {
        HideallPanels();
        if (_uiPanel)
        {
            _uiPanel.Show();
           
        }
        if(gameIsOn)
        {
            //we shld do this, as we are gng back to main menu and that is itself a starting panel of game
            if (_uiPanel.eui_Panel == EUI_PANEL.mainMenuE) 
            {
                IsPauseGame = false;
                LoadLevelByIndex(0);
            }
        }

    }
    void LoadLevelByIndex(int _levelIndex)
    {
        GameManager.sharedInstance.LoadByLevelIndex(_levelIndex);
    }
    public void ActivatePanel(EUI_PANEL _euipanel)
    {
        switch (_euipanel)
        {
            case EUI_PANEL.hudE:
                {
                    gameIsOn = true;
                    backGroundImage.SetActive(false);
                    break;
                }
            case EUI_PANEL.mainMenuE:
                {
                    gameIsOn = false;
                    backGroundImage.SetActive(true);
                    break;
                }
            case EUI_PANEL.settingsE:
                {
                    break;
                }
            case EUI_PANEL.gameOverE:
                {
                    break;
                }
            case EUI_PANEL.gameWinE:
                {
                    break;
                }
            case EUI_PANEL.pauseE:
                {
                    if (!gameIsOn)
                    {
                        IsPauseGame = !IsPauseGame;
                    }
                    break;
                }
            case EUI_PANEL.welcomeE:
                {
                    break;
                }
            //case EUI_PANEL.shop:
            //    {
            //        gameIsOn = false;
            //        break;
            //    }
            default:
                break;
        }
        UIPanel _uipanel = GetUIpanelFromEnum(_euipanel);
        ActivatePanel(_uipanel);
        if (_euipanel == EUI_PANEL.pauseE)
        {
            UIPanel hud_ = GetUIpanelFromEnum(EUI_PANEL.hudE);
            hud_.Show();
        }

    }
   
    UIPanel GetUIpanelFromEnum(EUI_PANEL euipanel)
    {
        foreach (UIPanel item in allpanelsList)
        {
            if (item.eui_Panel == euipanel)
            {
               //settingsPanel.SoundPlay("button");
                GameManager.sharedInstance.SoundPlay("button");
                return item;
            }
        }
        return null;
    }
    #endregion

    void Initialise()
    {
        HideallPanels();
      //  PlayerPrefs.GetInt(gameinputkey, GameInputs ? 1 : 0);
        //SoundInitialization();
        PlayerNameInit();
    }
    void HideallPanels()
    {
        foreach (UIPanel item in allpanelsList)
        {
            if (!item.isHidden)
            {
                item.Hide();
            }
           // backGroundImage.SetActive(true);
        }
    }
  
    public void DisplayHealth(float _health)
    {
        healthSilder.value = _health;
        healthFillBar.color = healthGradient.Evaluate(_health);
    }
   

    public void PowerUpFill(float _powerup, float _powerUpTimerText)
    {
        powerUpImage.fillAmount = _powerup;
        powerUpImage.color = healthGradient.Evaluate(_powerup);
        powerUpTimerText.text = _powerUpTimerText.ToString("0.0");
    }
    public void PowerUpName(string _powername)
    {
        powerUpname.text = _powername;
    }
    public void DisplayScore(int _score)
    {
        scoreText.text = "Score: " + _score.ToString();
    }
    public void StartGame()
    {
        if (GetCurrentSceneIndex() == 0)
        {
            NextLevel();
        }
        else
        {
            ActivatePanel(EUI_PANEL.hudE);
            gameIsOn = false;
            GameManager.sharedInstance.Play();
        }   
    }
    public void RestartGame()
    {
        GameManager.sharedInstance.Restart();
    }
    public void BackToMainPanel()
    {
        if (IsPauseGame)
            ActivatePanel(EUI_PANEL.pauseE);
        else
            ActivatePanel(EUI_PANEL.mainMenuE); 
    }

    public void ResumeGame()
    {
        IsPauseGame = false;
    }
    public void DoneBtn() //back from settings to game
    {
        if (gameIsOn)
        { ActivatePanel(EUI_PANEL.pauseE); }
        else
            ActivatePanel(EUI_PANEL.mainMenuE);
    }
    public void Back_HUD()
    {
        IsPauseGame = true;
    }


    //public Button ToggleBtn;
    //public GameObject joyStickObj;
    //string gameinputkey = "inputKey";
    // bool toggleInput;
    //public bool GameInputs
    //{
    //    get { return toggleInput; }
    //    set
    //    {
    //        toggleInput = value;

    //        ToggleBtn.gameObject.transform.GetComponentInChildren<TMP_Text>().text = toggleInput ? "JoyStick" : "KeyBoard";
    //        PlayerPrefs.SetInt(gameinputkey, toggleInput ? 1 : 0);
    //        joyStickObj.SetActive(toggleInput);
    //        UiGameInputs(toggleInput);
    //    }
    //}
    //public void UiGameInputs(bool _drag)
    //{
    //    GameManager.sharedInstance.GameInputs(_drag);
    //}
    //public void ControlToggle()
    //{
    //    GameInputs = !GameInputs;
    //    PlayerPrefs.GetInt(gameinputkey, GameInputs ? 1 : 0);
    //}

    #region Sound

    //public Slider volumeSlider;
    //public float volume;
    //public bool musiconoff = true;
    //public bool soundOnOff = true;

    //public Text musictext;
    //public Text soundtext;

    //string musickey = "music";
    //string soundkey = "sound";
    //string volumekey = "volume";
    
    
    
    //public bool SoundOnOff
    //{
    //    get
    //    {
    //        return soundOnOff;
    //    }
    //    set
    //    {
    //        soundOnOff = value;
    //        soundtext.text = soundOnOff  ? "Sound : On" : "Sound : Off";
    //    }
    //}

    //public bool MusicOnOff
    //{
    //    get
    //    {
    //        return musiconoff;
    //    }
    //    set
    //    {
    //        musiconoff = value;
    //        musictext.text = musiconoff ? "On" : "Off";
    //    }
    //}
    //public void SoundInitialization()
    //{
    //    int s = PlayerPrefs.GetInt(musickey);


    //    MusicOnOff = s == 1 ? true : false;

    //    int sound = PlayerPrefs.GetInt(soundkey);


    //    SoundOnOff = sound == 1 ? true : false;

    //    volume = PlayerPrefs.GetFloat(volumekey);
    //    volumeSlider.value = volume;
    //    SetVolume(volume);

    //    gameIsOn = true;
    //}
    //public void MusicBtn()
    //{
    //    MusicOnOff = !MusicOnOff;
    //    PlayerPrefs.SetInt(musickey, musiconoff ? 1 : 0);
    //    MusicPlayPause(musiconoff);
    //    SoundPlay("button");
    //}

    //public void SoundBtn()
    //{
    //    SoundOnOff = !SoundOnOff;

    //    PlayerPrefs.SetInt(soundkey, soundOnOff ? 1 : 0);

    //    SoundPlay("button");
    //}
    //void SoundPlay(string _soundname)
    //{
    //    GameManager.sharedInstance.SoundPlay(_soundname);
    //}
    //public void MusicPlayPause(bool _play)
    //{
    //    GameManager.sharedInstance.MusicAudio(_play);
    //}
    //public void MusicVolumeSlider(float _volume)
    //{
    //    volume = _volume;
    //    PlayerPrefs.SetFloat(volumekey, volume);
    //    SetVolume(volume);
    //}
    //public void SetVolume(float _volume)
    //{
    //    GameManager.sharedInstance.GSetVolume(_volume);
    //}
    #endregion

    #region player data
    //playername data
    string playerNameKey = "playername";
    string playername;
    public TMP_InputField playernameText;
    public TMP_Text showplayername;

    void PlayerNameInit()
    {
        if (PlayerPrefs.HasKey(playerNameKey))
        {
            
            if(GetCurrentSceneIndex() == 0)
            {
                ActivatePanel(EUI_PANEL.mainMenuE) ;
            }
           
            showplayername.text = "Welcome " + PlayerPrefs.GetString(playerNameKey, playername);
        }
        else
        {
            ActivatePanel(EUI_PANEL.welcomeE);
        }
    }
  
  
    public void EnterPlayerName(string _name)
    {
        playername = _name;
        playernameText.text =  playername;
        PlayerPrefs.SetString(playerNameKey, playername);
    }
   
  
    #endregion

    #region LevelInfo
    public void NextLevel()
    {
        GameManager.sharedInstance.NextLevel();

    }
    public void DisplayLevel(int display)
    {
        displayLevelText.text = "Level : " + display.ToString();

    }
    public void PreviousLevel()
    {
        GameManager.sharedInstance.PreviousLevel();
    }
    public int GetCurrentSceneIndex()
    {
        return GameManager.sharedInstance.GetCurrentSceneIndex();
    }

    public bool IsPrevLevelThere()
    {
        return GameManager.sharedInstance.IsPrevLevelThere();
    }
    public bool IsNextLevelThere()
    {
        return GameManager.sharedInstance.IsNextLevelThere();
    }

    void LoadButtonSetup()
    {
        nextLevelBtn.interactable = IsNextLevelThere();

        previousBtn.gameObject.GetComponentInChildren<TMP_Text>().text = IsPrevLevelThere() ? "Previous Level" : "Main Menu";
    }

    #endregion

  
}
public enum EUI_PANEL
{
    hudE,
    mainMenuE,
    settingsE,
    gameOverE,
    gameWinE,
    pauseE,
    welcomeE,
    levelSelectE,
    shop
}
