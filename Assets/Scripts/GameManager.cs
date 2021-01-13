using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    [SerializeField]
    private Gradient healthGradient;
    public GameData gameDataScript;
    public Player playerScript;
    public TurretE turretE;
    public UIManager uIManagerScript;
    private ObjectPoolManager particleEMScript;
    private CoinManager coinManagerScript;
    private LevelManager levelManagerScript;
    private ScoreManager scoreManagerScript;
    private PowerUpManager powerupManagerScript;
    private AudioManager audioManagerScript;
   EnemyHealth enemyHealthScript;
   public SettingsData settingsData;
    public GoldScript goldScript;
    public int goldValue;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            
            Destroy(gameObject);
        }
        playerScript = FindObjectOfType<Player>();
        uIManagerScript = FindObjectOfType<UIManager>();
        particleEMScript = FindObjectOfType<ObjectPoolManager>();
        coinManagerScript = FindObjectOfType<CoinManager>();
        levelManagerScript = FindObjectOfType<LevelManager>();
        scoreManagerScript = FindObjectOfType<ScoreManager>();
        powerupManagerScript = FindObjectOfType<PowerUpManager>();
        audioManagerScript = FindObjectOfType<AudioManager>();
        settingsData = FindObjectOfType<SettingsData>();
        goldScript = FindObjectOfType<GoldScript>();
        turretE = FindObjectOfType<TurretE>();
        settingsData = FindObjectOfType<SettingsData>();
    }
    public void YouWon()
    {
        playerScript.GameOver = true;
        SwitchPanel(EUI_PANEL.gameWinE);
        playerScript.GameWin();
        
    }

    public void DisplayHealth(float _health)
    {
        uIManagerScript.DisplayHealth(_health);
    }
    public void PlayEffect(Vector3 _spawnPosition)
    {
        particleEMScript.PlayEffect(_spawnPosition);
    }
    public void DisplayScore(int _score)
    {
        uIManagerScript.DisplayScore(_score);
    }
    public void ReduceChildCount()
    {
        coinManagerScript.ReduceChildCount();
    }
    public void AddScore(int _score)
    {
        scoreManagerScript.AddScore(_score);
    }
    public void Restart()
    {
        playerScript.Restart();
        SwitchPanel(EUI_PANEL.hudE);
        coinManagerScript.ResetCoins();
        powerupManagerScript.Replay();
        enemyHealthScript.Replay();
        turretE.Replay();
    }
    public Color GetHealthGradient(float val)
    {
        return healthGradient.Evaluate(val);
    }
    public void Play()
    {
        playerScript.Play();
        SwitchPanel(EUI_PANEL.hudE);
    }
    public void GameOver()
    {
        turretE.GameOverCheck();
        Invoke("ActivateGameOver", 0.9f);
    }
    void ActivateGameOver()
    {
        SwitchPanel(EUI_PANEL.gameOverE);
    }
    public void SwitchPanel(EUI_PANEL euipanel)
    {
        if (uIManagerScript)
        {
            uIManagerScript.ActivatePanel(euipanel);
        }
    }
    public void PowerUpFill(float _powerup,float _powerUpTimerText)
    {
        uIManagerScript.PowerUpFill(_powerup, _powerUpTimerText);
    }
    public void PowerUpName(string _powername)
    {
        uIManagerScript.PowerUpName(_powername);
    }
    public void NextLevel()
    {
        levelManagerScript.LoadNextLevel();
    }

    public void LoadByLevelIndex(int _levelIndex)
    {
        levelManagerScript.LoadLevelByIndex(_levelIndex);
    }
    public void PreviousLevel()
    {
        levelManagerScript.LoadPreviousLevel();
    }

    public int GetCurrentSceneIndex()
    {
        return levelManagerScript.GetCurrentSceneIndex();
    }

    public bool IsPrevLevelThere()
    {
        return levelManagerScript.IsPrevLevelThere();
    }
    public bool IsNextLevelThere()
    {
        return levelManagerScript.IsNextLevelThere();
    }
    public void ToggleControllerType(bool _drag)
    {
        playerScript.GameInputs(_drag);
    }
    public bool ISGAMEOVERFUNC()
    {
        return playerScript.GameOver;
    }
    
    #region SoundManager
   
    bool soundInfo;
    public void SoundPlay(string _soundname)
    {
        if (settingsData.SoundInfo)
            audioManagerScript.PlaySound(_soundname);
    }

    public void MusicStatus(bool _play)
    {
        audioManagerScript.MusicPlayPause(_play);
    }
    public void GSetMusicVolume(float _volume)
    {
        audioManagerScript.MusicVolume(_volume);
    }
    public void GMainSoundVolume(float _vol)
    {
        audioManagerScript.MainSoundVolue(_vol);
    }
    
    #endregion
}
