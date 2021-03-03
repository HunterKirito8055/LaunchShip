using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopSelectOnContent : MonoBehaviour
{
    public GameObject shopEmptyCardPrefab; // empty prefab of Card to be given
    public List<ShopItem_SO> ScriptableShopCards; // Assign whatever the total scriptable objects which in turn has an object of its own
    public ShopItemCardNew currentSelectedCardRef, lastSelectedCardRef;
    List<ShopItemCardNew> shopItemNewCards;

    private void Start()
    {
        for (int i = 0; i < ScriptableShopCards.Count; i++)
        {
            GameObject newShopItem = Instantiate(shopEmptyCardPrefab, transform);
            newShopItem.name = ScriptableShopCards[i].name.ToString();

            ShopItemCardNew shopItemCardNew = newShopItem.GetComponent<ShopItemCardNew>();

            if (ScriptableShopCards[i].defaultPlayer)
            {
                currentSelectedCardRef = shopItemCardNew;
            }
            shopItemCardNew.CommenceSubscribing(ScriptableShopCards[i], BuyingTransaction, SelectingCard);
        }
    }
    void BuyingTransaction(int cost, Action purchaseResult)
    {
        ItemPurchaseStatus purchaseStatus;
        GoldScript.goldInstance.SpendGold(cost, out purchaseStatus);
        switch (purchaseStatus)
        {
            case ItemPurchaseStatus.failed:
                print("Transaction Failed");
                break;
            case ItemPurchaseStatus.noGold:
                print("Not Enough Gold");
                break;
            case ItemPurchaseStatus.success:
                purchaseResult.Invoke();
                print("Transaction Successful");
                break;
            case ItemPurchaseStatus.processing:
                print("Transaction Processing");
                break;
            default:
                break;
        }
    }
    void SelectingCard(ShopItemCardNew clickeditem)
    {
        currentSelectedCardRef.CardSelectionStatus = false;//setting previous card to false
        currentSelectedCardRef = clickeditem;// assigning new card which is clicked now
        currentSelectedCardRef.CardSelectionStatus = true;// and its status to true
    }

}


