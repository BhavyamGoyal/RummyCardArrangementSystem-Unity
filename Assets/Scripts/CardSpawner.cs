using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner 
{
    public JsonDataClass cardsData;
    public CardAssets assets;
    GameObject cardPrefab;
    public CardSpawner(CardAssets assets, GameObject prefab)
    {
        this.assets = assets;
        this.cardPrefab = prefab;
    }
    public IEnumerator LoadCards(Action<JsonDataClass, GameObject> callback, GameObject group)
    {
        Debug.Log("[HandCardManager]Started Loading Cards");
        ResourceRequest LoadRequest = Resources.LoadAsync("StartingCards");
        yield return LoadRequest;
        TextAsset data = LoadRequest.asset as TextAsset;
        Debug.Log("[HandCardManager]Finished Loading Data From JSON File " + data.text);

        if (data != null)
        {
            JsonDataClass cardsData = JsonUtility.FromJson<JsonDataClass>(data.text);

            callback(cardsData, group);
        }

    }

    public void setCards(JsonDataClass cardsData,GameObject group)
    {
        
        this.cardsData = cardsData;
        
       
        for (int i = 0; i < cardsData.value.cards.Count; i++)
        {
            Sprite cardSprite = null;
            SUITS suit = SUITS.SPAIDS;
            char[] card = cardsData.value.cards[i].ToCharArray();
            int num = int.Parse(cardsData.value.cards[i].Substring(0, card.Length - 1));
            switch (card[card.Length - 1])
            {
                case 's':
                    suit = SUITS.SPAIDS;
                    cardSprite = assets.GetCardSprite(SUITS.SPAIDS, num);
                    break;
                case 'c':
                    suit = SUITS.CLUBS;
                    cardSprite = assets.GetCardSprite(SUITS.CLUBS, num);
                    break;
                case 'h':
                    suit = SUITS.HEARTS;
                    cardSprite = assets.GetCardSprite(SUITS.HEARTS, num);
                    break;
                case 'd':
                    suit = SUITS.DIAMONDS;
                    cardSprite = assets.GetCardSprite(SUITS.DIAMONDS, num);
                    break;
            }
            CardView cardView = GameObject.Instantiate(cardPrefab, group.transform).GetComponent<CardView>();
            cardView.SetCardData(num, suit,cardSprite);
        }
    }
}
