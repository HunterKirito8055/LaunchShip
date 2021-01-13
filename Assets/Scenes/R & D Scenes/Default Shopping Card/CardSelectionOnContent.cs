using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionOnContent : MonoBehaviour
{
    public ECharacterType defaultSelected;
    public GameObject currentSelectedCharacter;
      
     ItemCard currentSelectedCard;
    public ItemCard CurrentSelectedCard
    {
        get { return currentSelectedCard; }

        set
        {
            currentSelectedCard = value;
            defaultSelected = currentSelectedCard.cardType;
            currentSelectedCharacter = currentSelectedCard.characterPrefab;
        }
    }
    public ItemCard previousSelectedCard;
    public GameObject prefabedCard;
    public List<GameObject> characterPrefabs;

    private void Start()
    {
        foreach (var shapesCards in characterPrefabs)
        {
            GameObject newPrefabedObj = Instantiate(prefabedCard, transform);
            CharacterType characterCardRef = shapesCards.GetComponent<CharacterType>();
            ItemCard itemCardRef = newPrefabedObj.GetComponent<ItemCard>();

            bool btnStatus = defaultSelected == characterCardRef.eCharacterType;
            if (btnStatus)
            {
                currentSelectedCard = itemCardRef;
                currentSelectedCharacter = newPrefabedObj;
            }
            itemCardRef.Initialise(/*characterCardRef.eCharacterType*/onCharacterSelect,btnStatus,shapesCards);
        }
    }

    void onCharacterSelect(ItemCard clickedItemCard)
    {
        //print("You clicked on "+itemCard.name);
        previousSelectedCard = CurrentSelectedCard;
        CurrentSelectedCard = clickedItemCard;
        if (previousSelectedCard)
        {
            previousSelectedCard.WhenClicked(false);
        }

        if(CurrentSelectedCard)
        {
            CurrentSelectedCard.WhenClicked(true);
        }
    }
}
