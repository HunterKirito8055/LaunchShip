using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    [SerializeField]
    private Gradient healthGradient;
    public PlayerPrefsRegister gameDataScript;
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
    public string sharedTimeforClicking;
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
        if (playerScript == null) playerScript = FindObjectOfType<Player>();
        if (uIManagerScript == null) uIManagerScript = FindObjectOfType<UIManager>();
        if (particleEMScript == null) particleEMScript = FindObjectOfType<ObjectPoolManager>();
        if (coinManagerScript == null) coinManagerScript = FindObjectOfType<CoinManager>();
        if (levelManagerScript == null) levelManagerScript = FindObjectOfType<LevelManager>();
        if (scoreManagerScript == null) scoreManagerScript = FindObjectOfType<ScoreManager>();
        if (powerupManagerScript == null) powerupManagerScript = FindObjectOfType<PowerUpManager>();
        if (audioManagerScript == null) audioManagerScript = FindObjectOfType<AudioManager>();
        if (settingsData == null) settingsData = FindObjectOfType<SettingsData>();
        if (goldScript == null) goldScript = FindObjectOfType<GoldScript>();
        if (turretE == null) turretE = FindObjectOfType<TurretE>();
        if (settingsData == null) settingsData = FindObjectOfType<SettingsData>();
        if (enemyHealthScript == null) enemyHealthScript = FindObjectOfType<EnemyHealth>();
    }
    public void YouWon()
    {
        playerScript.gameWon = true;
        SwitchPanel(EUI_PANEL.gameWinE);
        playerScript.GameWin();

        // we started having enemies after few level only
        //so while in few levels, we dont have turret object
        if (turretE)
            turretE.CheckGameOver();
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
        if (coinManagerScript)
        {
            Debug.Log("coinManagerScript Restart");
            coinManagerScript.ResetCoins();
        }
        if (powerupManagerScript)
        {
            Debug.Log("powerupManagerScript Restart");
            powerupManagerScript.Replay();
        }
        if (enemyHealthScript)
        {
            Debug.Log("enemyHealthScript Restart");
            enemyHealthScript.Replay();
        }
        if (turretE)
        {
            Debug.Log("turretE Restart");
            turretE.Replay();
        }
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
        if (turretE)
            turretE.CheckGameOver();
        Invoke("ActivateGameOver", 0.9f);
        playerScript.GameOver = true;
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
    public void PowerUpFill(float _powerup, float _powerUpTimerText)
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
    public void ToggleControllerType(bool MblOrPc)
    {
        if (playerScript)
            playerScript.GameInputs(MblOrPc);
    }
    public bool ISGAMEOVERFUNC()
    {
        //playerScript.CanShootSet(false);
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
