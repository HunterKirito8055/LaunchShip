using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSpanScript : MonoBehaviour
{

    public TimeSpan ts, timeleft;
    public Text textField;
    public Button btn;
    bool isStarted;
    public string retrievedKey;
    string timerKey = "TimeSpanKey";
    public bool IsStarted
    {
        get
        {
            return isStarted;
        }
        set
        {
            if (isStarted != value)
                isStarted = value;
            if (isStarted)
            {
                btn.interactable = false;
            }
            else
            {
                textField.text = "Click To Get Gold";
                btn.interactable = true;
            }
        }
    }
    private void Start()
    {
        retrievedKey = PlayerPrefs.GetString(timerKey, "default");
        IsStarted = retrievedKey != "default" ? true : false;

        if (retrievedKey!="default")
        {
            ts = TimeSpan.Parse(retrievedKey);
        }
    }
    private void Update()
    {
        if (IsStarted)
        {
            DisplayTimer();
        }
    }
    void DisplayTimer()
    {
        timeleft = ts - DateTime.Now.TimeOfDay;
        // textField.text = timeleft.ToString();
        textField.text = "Get gold in :" + timeleft.Hours.ToString("00") + ":" + timeleft.Minutes.ToString("00") + ":" + timeleft.Seconds.ToString("00");

        if (timeleft.TotalSeconds <= 0)
        {
            IsStarted = false;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteKey(timerKey);
        }
    }
    public void onClickGoldBtn()
    {
       SetTime();
        GoldBonus();
    }
    void SetTime()
    {
        IsStarted = true;
        ts = new TimeSpan(00, 00, 15);
        ts = ts + DateTime.Now.TimeOfDay;
        //textField.text = ts.ToString();
        GameManager.sharedInstance.gameDataScript.SavetimeSpan = ts;
       PlayerPrefs.SetString(timerKey, ts.ToString());
    }
    void GoldBonus()
    {
       int count = UnityEngine.Random.Range(1, 4);
        count += count * 5;
        GameManager.sharedInstance.goldScript.AddGoldBonus(count);
    }
}//class
