using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSpanScript : MonoBehaviour
{
    //[VectorLabels("Date", "Hours", "Minutes", "Seconds")]
    //public Vector4 addAllotedTimeHere;
    [Range(0, 31)] public int Date;
    [Range(0, 24)] public int Hours;
    [Range(0, 60)] public int Minutes;
    [Range(0, 60)] public int Seconds;
    DateTime futureTime;
    public TimeSpan timeToWait;/*= new TimeSpan(00, 00, 02, 00)*/
    public TimeSpan timeleft;
    //public TimeSpan ts;
    public Text textField;
    public Button btn;
    bool isStarted;
    //public string sharedTime ;
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
            {
                isStarted = value;
                if (isStarted)
                {
                    futureTime = DateTime.Parse(GameManager.sharedInstance.sharedTimeforClicking);
                    btn.interactable = false;
                }
                else if (!isStarted)
                {
                    textField.text = "Click To Get Gold";
                    btn.interactable = true;
                }
                //Debug.Log("interactible = " + btn.interactable);
            }
        }
    }
  
    private void Start()
    {
       
        if (Date+Hours+Minutes+Seconds <=5)
        {
            Seconds = 5;
        }
        GameManager.sharedInstance.sharedTimeforClicking = SavedDateandTime;
        IsStarted = GameManager.sharedInstance.sharedTimeforClicking != "default" ? true : false;
        btn.onClick.AddListener(onClickGoldBtn);
        // retrievedKey = PlayerPrefs.GetString(timerKey, "default");
        //if (retrievedKey != "default")
        //{
        //    ts = TimeSpan.Parse(SavedDateandTime);
        //}
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
        timeleft = futureTime - DateTime.Now;
        textField.text = "Get gold in: " + timeleft.Days.ToString("00") + ":" + timeleft.Hours.ToString("00") + ":" + timeleft.Minutes.ToString("00") + ":" + timeleft.Seconds.ToString("00");

        if (timeleft.TotalSeconds <= 0)
        {
            IsStarted = false;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteKey(timerKey);
        }
        //Debug.Log(futureTime);
        // textField.text = timeleft.ToString();
    }

    void TimeToWait()
    {
        timeToWait = new TimeSpan(Date, Hours, Minutes, Seconds);
        DateTime dt = DateTime.Now;
        DateTime future = dt.Add(timeToWait);

        SavedDateandTime = future.ToString();
        GameManager.sharedInstance.sharedTimeforClicking = future.ToString();
        IsStarted = true;
    }
    public string SavedDateandTime
    {
        get
        {
            //return GameManager.sharedInstance.gameDataScript.savetimeSpan;
            return PlayerPrefs.GetString(PlayerPrefsRegister.savetimeSpan);
        }
        set
        {
            //GameManager.sharedInstance.gameDataScript.savetimeSpan = value;
            string t = value;
            PlayerPrefs.SetString(PlayerPrefsRegister.savetimeSpan,t);
        }
    }
    public void onClickGoldBtn()
    {
        btn.interactable = false;
        GoldBonus();
        TimeToWait();
        //SetTime();
    }
    void GoldBonus()
    {
        int count = UnityEngine.Random.Range(1, 4);
        count += count * 100;
        GameManager.sharedInstance.goldScript.AddGoldBonus(count);
    }
   
}//class
