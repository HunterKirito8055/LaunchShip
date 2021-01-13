using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public List<ShopItemClosure>  shopItemList;
    private void Start()
    {
        for(int i = 0;i<shopItemList.Count;i++)
        {
            GameObject newShopitemObj = Instantiate(shopItemPrefab, transform);
            newShopitemObj.GetComponent<ShopCard>().Initialize(shopItemList[i],ReturnGoldAmount);
        }
    }
    void ReturnGoldAmount(int amount)
    {
        GoldScript.goldInstance.SpendGold(amount);
        print(GoldScript.goldInstance.GOLDAMOUNT);
    }
}
