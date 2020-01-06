using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardManager : MonoSingleton<HandCardManager>
{
    public List<Transform> groupList = new List<Transform>();
    public GameObject groupCreatorView;
    CardSpawner spawner;
    public GameObject dummyCard;
    public CardAssets assets;
    public RectTransform handCards;
    public GameObject groupPrefab, cardPrefab;
    public CardView selectedCard;
    public Canvas canvas;
    Vector2 groupCreatorDimensions;
    public void Start()
    {
        spawner = new CardSpawner(assets, cardPrefab);
        GameObject group = GameObject.Instantiate(groupPrefab, handCards);
        groupList.Add(group.transform);
        StartCoroutine(spawner.LoadCards(spawner.setCards, group));
        RectTransform creator = groupCreatorView.GetComponent<RectTransform>();
        groupCreatorDimensions.x = creator.rect.width;
        groupCreatorDimensions.y = creator.rect.height;
    }
    public void SelectCard(CardView card)
    {
        selectedCard = card;
        card.CardSelected();
        dummyCard.transform.SetParent(selectedCard.transform.parent);
        dummyCard.transform.SetSiblingIndex(selectedCard.transform.GetSiblingIndex());
        selectedCard.transform.SetParent(canvas.transform);
        dummyCard.SetActive(true);
    }

    public void MoveSelectedCard(Vector2 position)
    {
        if (selectedCard != null)
        {
            ActivateCreateGroupUI(true);
            selectedCard.transform.position = position;
            CheckForNextCard();
            CheckForPreviousCard();

        }
    }

    private void CheckForNextCard()
    {
        Transform nextCard = null;
        int dummyIndex = dummyCard.transform.GetSiblingIndex();
        if (dummyIndex < dummyCard.transform.parent.childCount - 1)
        {
            nextCard = dummyCard.transform.parent.GetChild(dummyIndex + 1);
            if (selectedCard.transform.position.x > nextCard.position.x)
            {
                dummyCard.transform.SetSiblingIndex(nextCard.GetSiblingIndex());
            }
        }
        else
        {
            int groupIndex = dummyCard.transform.parent.GetSiblingIndex();
            if (groupIndex < dummyCard.transform.parent.parent.childCount - 1)//check if allready in last group;
            {
                Transform nextGroup = dummyCard.transform.parent.parent.GetChild(groupIndex + 1);
                if (nextGroup.childCount > 0)
                {
                    Transform firstCardOfNextGroup = nextGroup.GetChild(0);
                    float dis = (firstCardOfNextGroup.position.x + dummyCard.transform.position.x) / 2;
                    if (selectedCard.transform.position.x > dis)
                    {
                        dummyCard.transform.SetParent(nextGroup);
                        dummyCard.transform.SetSiblingIndex(0);
                    }
                }

            }
        }

    }

    private void CheckForPreviousCard()
    {
        Transform prevCard = null;
        int dummyIndex = dummyCard.transform.GetSiblingIndex();
        if (dummyIndex > 0)
        {
            prevCard = dummyCard.transform.parent.GetChild(dummyIndex - 1);
            if (selectedCard.transform.position.x < prevCard.position.x)
            {
                dummyCard.transform.SetSiblingIndex(prevCard.GetSiblingIndex());
            }
        }
        else {
            int groupIndex = dummyCard.transform.parent.GetSiblingIndex();
            if (groupIndex > 0)
            {
                Transform prevGroup = dummyCard.transform.parent.parent.GetChild(groupIndex - 1);
                if (prevGroup.childCount > 0) {
                    Transform lastCardOfPrevGroup = prevGroup.GetChild(prevGroup.childCount - 1);
                    float dis = (lastCardOfPrevGroup.position.x + dummyCard.transform.position.x) / 2;
                    if (selectedCard.transform.position.x < dis)
                    {
                        dummyCard.transform.SetParent(prevGroup);
                        dummyCard.transform.SetSiblingIndex(prevGroup.childCount - 1);
                    }
                }

            }
        }

    }

    public void OnCardRelease()
    {
        if (selectedCard != null)
        {
            dummyCard.SetActive(false);
            if (CanCreateGroup() && selectedCard.transform.position.x > (groupCreatorView.transform.position.x - (groupCreatorDimensions.x)) && selectedCard.transform.position.y < (groupCreatorView.transform.position.y + (groupCreatorDimensions.y)))
            {
                selectedCard.transform.SetParent(AddGroup());
            }
            else
            {
                ActivateCreateGroupUI(false);
                selectedCard.transform.SetParent(dummyCard.transform.parent);
                selectedCard.transform.SetSiblingIndex(dummyCard.transform.GetSiblingIndex());
            }
            selectedCard.CardReleased();
            dummyCard.transform.SetParent(null);
            selectedCard = null;
            CheckForEmptyGroups();
        }
    }

    public Transform AddGroup()
    {
        groupList.Add(GameObject.Instantiate(groupPrefab, handCards).transform);
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
                break;
            }
        }
    }

    public bool CanCreateGroup()
    {
        if (groupList.Count <= 4)
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
