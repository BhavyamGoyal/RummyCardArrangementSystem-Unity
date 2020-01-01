using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollision : MonoBehaviour
{
    //To detect the card collision of the left card collider
    public CardView parentCardView;
    bool selected = false;

    public void SetSelected(bool selected)
    {
        this.selected = selected;
    }
    private void Start()
    {
        parentCardView = GetComponentInParent<CardView>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (selected)
        {
            CardCollision cardCollided = collider.gameObject.GetComponent<CardCollision>();
            if (cardCollided != null)
            {
                parentCardView.SetDummyParent(cardCollided.parentCardView.gameObject.transform.parent);
                parentCardView.SetCardIndexToFill(cardCollided.parentCardView.transform.GetSiblingIndex());
            }
            else if (collider.gameObject.GetComponent<GroupView>() != null)
            {
                Debug.Log("[CardCollision]Collided with a group");
                parentCardView.SetDummyParent(collider.gameObject.transform);
                parentCardView.SetCardIndexToFill(collider.transform.childCount);
            }
        }

    }
}
