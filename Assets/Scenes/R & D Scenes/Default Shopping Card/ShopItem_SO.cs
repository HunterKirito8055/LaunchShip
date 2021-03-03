using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New ShopItem", menuName = "Create New ShopItem")]
public class ShopItem_SO : ScriptableObject
{
    public string _name = "";
    public GameObject playerObject;
    public Sprite cardImage;
    public int cost;
    public bool defaultPlayer;
    public bool isPurchased;
}//class



//public enum ShopItem
//{
////cube,sphere,cylinder,capsule    
//itemImage,defaultPlayer,isPurchased
//}
