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
       private set
        {
            goldAmount = value;
            if (goldAmount > 0)
            {
                PlayerPrefs.SetInt(goldKey, goldAmount);
                DisplayGoldAmount();
            }
        }
    }

    private void Awake()
    {
        goldInstance = this;
        GOLDAMOUNT = PlayerPrefs.GetInt(goldKey);
    }
    void Start()
    {
        RetrieveDefaultGold();
        DisplayGoldAmount();
    }
    void RetrieveDefaultGold()
    {
        GOLDAMOUNT = PlayerPrefs.GetInt(goldKey, 1000);
    }
    public void DisplayGoldAmount()
    {
        goldShop.text = "Gold :" + GOLDAMOUNT.ToString("00,000");
        goldField.text = "Gold :" + GOLDAMOUNT.ToString("00,000");
    }
    public void AddGoldBonus(int count)
    {
        GOLDAMOUNT += count;
    }
    
    public void SpendGold(int _howmuch,out ItemPurchaseStatus status)
    {
        int goldLeft = GOLDAMOUNT - _howmuch;
        if (goldLeft >= 0)
        {
            //we can buy anything checking purchase value conditions
            GOLDAMOUNT = goldLeft;
            // DisplayGoldAmount();
            status = ItemPurchaseStatus.success;
        }
        else
        {
            //print("No Gold Enough, Get more Gold");
            status = ItemPurchaseStatus.noGold;
        }
    }

}
public enum ItemPurchaseStatus
{
    failed, noGold, success, processing
}
