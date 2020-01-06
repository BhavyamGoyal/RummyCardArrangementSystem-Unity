using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    CardObject card;
    public int childIndex;

    public void SetCardData(int num,SUITS suit,Sprite img)
    {
        
        if (card == null)
        {
            card = new CardObject();
            card.cardImage = GetComponent<Image>();
        }
        card.number = num;
        card.suit = suit;
        card.cardImage.sprite = img;
    }
    public void CardSelected()
    {
        Color color = card.cardImage.color;
        color.a = .7f;
        card.cardImage.color = color;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z);
    }
    public void CardReleased()
    {
        Color color = card.cardImage.color;
        color.a = 1f;
        card.cardImage.color = color;
    }
}
