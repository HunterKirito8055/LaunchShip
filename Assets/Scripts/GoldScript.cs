using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldScript : MonoBehaviour
{
    public static GoldScript goldInstance;
    public Text goldField;
    public Text goldShop;
    string goldKey = "GoldCount";
   public int goldAmount;
    public int GOLDAMOUNT
    {
        get { return goldAmount; }
        set
        {
            goldAmount = value;
            if (goldAmount > 0)
            {
                DisplayGoldAmount();
            }
        }
    }

    private void Awake()
    {
        goldInstance = this;
    }
    void Start()
    {
        GOLDAMOUNT = PlayerPrefs.GetInt(goldKey);
      DisplayGoldAmount();
    }

   public void DisplayGoldAmount()
    {
        goldField.text = "Gold :" + GOLDAMOUNT.ToString("000,00");
        goldShop.text = "Gold :" + GOLDAMOUNT.ToString("000,00");
    }
   public  void AddGoldBonus(int count)
    {
        GOLDAMOUNT += count ;
        PlayerPrefs.SetInt(goldKey, GOLDAMOUNT);
       //DisplayGoldAmount();
    }
    public void SpendGold(int _howmuch)
    {
        int goldLeft = GOLDAMOUNT - _howmuch;
        if(goldLeft>=0)
        {
            //we can buy anything checking purchase value conditions
            GOLDAMOUNT = goldLeft;
            PlayerPrefs.SetInt(goldKey, GOLDAMOUNT);
           // DisplayGoldAmount();
        }
        else
        {
            print("No Gold Enough, Get more Gold");
        }
    }
   
}
