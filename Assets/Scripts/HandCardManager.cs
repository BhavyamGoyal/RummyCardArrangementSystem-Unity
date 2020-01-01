using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardManager : MonoSingleton<HandCardManager>
{
    // manages all the cards and groups 
    //for now maximum 4 groups can be created
    public int groupCount = 1;
    public List<GroupView> groupList = new List<GroupView>();
    public GameObject groupCreatorView;
    public JsonDataClass cardsData;
    public GameObject dummyCard;
    public CardAssets assets;
    public RectTransform handCards;
    public GameObject groupPrefab, cardPrefab;


    public void Start()
    {
        StartCoroutine(LoadCards(setCards));
    }

    public Transform AddGroup()
    {
        groupList.Add(GameObject.Instantiate(groupPrefab, handCards).GetComponent<GroupView>());
        return groupList[groupList.Count - 1].gameObject.transform;
    }

    public void CheckForEmptyGroups()
    {
        for (int i = 0; i < groupList.Count; i++)
        {
            if (groupList[i].transform.childCount == 0)
            {
                Destroy(groupList[i].gameObject);
                groupList.RemoveAt(i);
            }
        }
    }

    private void setCards(JsonDataClass cardsData)
    {
        GameObject group;
        this.cardsData = cardsData;
        group = GameObject.Instantiate(groupPrefab, handCards);
        groupList.Add(group.GetComponent<GroupView>());
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
            Image cardImage = GameObject.Instantiate(cardPrefab, group.transform).GetComponent<Image>();
            cardImage.sprite = cardSprite;
            cardImage.gameObject.GetComponent<CardView>().SetCardData(num, suit, dummyCard);
        }
    }


    public static IEnumerator LoadCards(Action<JsonDataClass> callback)
    {
        Debug.Log("[HandCardManager]Started Loading Cards");
        ResourceRequest LoadRequest = Resources.LoadAsync("StartingCards");
        yield return LoadRequest;
        TextAsset data = LoadRequest.asset as TextAsset;
        Debug.Log("[HandCardManager]Finished Loading Data From JSON File " + data.text);

        if (data != null)
        {
            JsonDataClass cardsData = JsonUtility.FromJson<JsonDataClass>(data.text);

            callback(cardsData);
        }

    }

    public bool CanCreateGroup()
    {
        if (groupCount <= 4)
        {
            return true;
        }
        return false;
    }

    public void ActivateCreateGroupUI(bool val)
    {
        if (val && groupList.Count < 4)
        {
            groupCreatorView.gameObject.SetActive(val);
        }
        else
        {
            groupCreatorView.gameObject.SetActive(false);
        }
    }
}
