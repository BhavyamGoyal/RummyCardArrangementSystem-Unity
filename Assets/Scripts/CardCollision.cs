using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollision : MonoBehaviour
{
    public CardView parentCardView;
    //public IgnoreCollider ignoreCollider;
    bool selected = false;
    //  [Inject] private TableService tableService;

    public void SetSelected(bool selected)
    {
        this.selected = selected;
    }
    private void Start()
    {
        parentCardView = GetComponentInParent<CardView>();
        //tableService = parentCardView.GetTableService();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (selected)
        {
            CardCollision cardCollided = collider.gameObject.GetComponent<CardCollision>();
            if (cardCollided != null)
            {
                parentCardView.SetDummyParent(cardCollided.parentCardView.gameObject.GetComponent<CardView>().GetParent());
                parentCardView.SetCardIndexToFill(cardCollided.parentCardView.transform.GetSiblingIndex());//, cardCollided.parentCardView.transform.parent.GetSiblingIndex());
                                                                                                           //  Debug.Log("<color=blue> NEW GROUP FUCKING INDEX CAN FUCKING CREATE GROUP</color>"+ cardCollided.parentCardView.transform.parent.GetSiblingIndex());
                                                                                                           //parentCardView.SetCardGroup(collision.gameObject.GetComponent<HandCardView>().GetGroupID());
            }
            else if (collider.gameObject.GetComponent<GroupView>() != null)
            {
                parentCardView.SetDummyParent(collider.gameObject.transform);
                parentCardView.SetCardIndexToFill(collider.transform.childCount);//, collider.gameObject.transform.GetSiblingIndex());
                                                                                 //parentCardView.SetCardIndexToFill(cardCollided.parentCardView.gameObject.transform.GetSiblingIndex());
            }


            //if (collider.gameObject.GetComponent<GroupCreatorView>() != null)
            //{
            //    parentCardView.CanCreateGroupBool(true);
            //}
        }

    }
}
