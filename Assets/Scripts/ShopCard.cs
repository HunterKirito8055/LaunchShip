using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    public TMP_Text text;
    public Image image;
    public Button btn;
    public void Initialize(ShopItemClosure shopItem, Action<int> action)
    {
        text.text = shopItem.cost.ToString() + " Gold To Buy";
        image.sprite = shopItem.itemSprite;
        btn.onClick.AddListener(() => { action(shopItem.cost); });
    } 
    //public void Initialize(int amount,Sprite _sprite)
    //{
    //    text.text = amount.ToString() + " Gold To Buy";
    //    image.sprite = _sprite;
    //}

}

[System.Serializable]
public class ShopItemClosure
{
    public Sprite itemSprite;
    public int cost;
}
