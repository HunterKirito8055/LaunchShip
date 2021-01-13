using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    public GameObject characterPrefab;
    public Text cardName, btnText;
    public Button btn;
    public ECharacterType cardType;
    bool btnStatus;
    public bool BtnStatus { get { return btnStatus; }
        set
        {
            btnStatus = !value;
            btn.interactable = btnStatus;
            btnText.text = btnStatus ? "Select" : "Now Selected";
        }
    }
    public void Initialise(/*ECharacterType charEnum,*/Action<ItemCard> action, bool _btnStatus,GameObject _charaPrefab)
    {
        cardType = _charaPrefab.GetComponent<CharacterType>().eCharacterType;
        characterPrefab = _charaPrefab;
        BtnStatus = _btnStatus;
        cardName.text = cardType.ToString();
        name = cardName.text;
        btn.onClick.AddListener(()=> { action(this/*GetComponent<ItemCard>()*/); });
    }
    public void WhenClicked(bool status)
    {
        BtnStatus = status;
    }
}
