using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCardNew : MonoBehaviour
{
    public Text btnText;
    public Image image;
    public Button buyBtn;
    public ShopItem_SO shopItem_SO;

    Action<int, Action> onBuyItem;
    Action<ShopItemCardNew> onSelect;

    public ShopItem_SO ShoppingItem
    {
        get { return shopItem_SO; }
        set
        {
            shopItem_SO = value;
            image.sprite = shopItem_SO.cardImage;
            if (shopItem_SO.isPurchased)
            {
                buyBtn.onClick.AddListener(() => { onSelect.Invoke(this); });
                CardSelectionStatus = shopItem_SO.defaultPlayer;
            }
            else
            {
                buyBtn.onClick.AddListener(() =>
                {
                    onBuyItem.Invoke(shopItem_SO.cost, ProcessPurchaseTransaction);
                });
                btnText.text = "Buy for " + shopItem_SO.cost + "Gold";
            }
        }
    }
    public void CommenceSubscribing(ShopItem_SO _item, Action<int, Action> _buyAction, Action<ShopItemCardNew> _selectionAction)
    {
        ShoppingItem = _item;
        onBuyItem = _buyAction;
        onSelect = _selectionAction;
    }
    //this success is run when player clicked on BUY .
    // this is subscribed from shopSelectOnContent script
    void ProcessPurchaseTransaction()
    {
        buyBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.AddListener(() => { onSelect.Invoke(this); });
        ShoppingItem.isPurchased = true;
        btnText.text = "Select";
        print("Purchase Made Successful");
    }

    bool cardSelectionStatus;
    public bool CardSelectionStatus
    {
        get { return cardSelectionStatus; }
        set
        {
            cardSelectionStatus = value;
            btnText.text = cardSelectionStatus ? "Selected" : "Select";
            //if card already selected, then btn should not be interactible
            buyBtn.interactable = !cardSelectionStatus;
            ShoppingItem.defaultPlayer = cardSelectionStatus;
        }
    }
    //public GameObject characterPrefab;
    //public Text cardName, btnText;
    //public Button btn;
    //public ShopItem cardType;
    //bool btnStatus;
    //public bool BtnStatus { get { return btnStatus; }
    //    set
    //    {
    //        btnStatus = !value;
    //        btn.interactable = btnStatus;
    //        btnText.text = btnStatus ? "Select" : "Now Selected";
    //    }
    //}
    //public void Initialise(/*ECharacterType charEnum,*/Action<ShopItemCardNew> action, bool _btnStatus,GameObject _charaPrefab)
    //{
    //    cardType = _charaPrefab.GetComponent<SelectionType>().selectionType;
    //    characterPrefab = _charaPrefab;
    //    BtnStatus = _btnStatus;
    //    cardName.text = cardType.ToString();
    //    name = cardName.text;
    //    btn.onClick.AddListener(()=> { action(this/*GetComponent<ItemCard>()*/); });
    //}
    //public void WhenClicked(bool status)
    //{
    //    BtnStatus = status;
    //}
}
