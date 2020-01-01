using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //every Card has this script on it 
    //used to control the card drag and its positions
    //sets the position of the dummy card object while the card is being dragged.
    private int groupID;
    private bool canCreateGroup;
    private Image image;
    private Vector3 prevDragPos;
    private Vector3 dragPos;
    private RectTransform canvas;
    private GameObject dummyObject;

    public CardCollision leftCollider;
    public SUITS cardSuit;
    public int cardNumber;

    private void Start()
    {
        canvas = GameObject.FindObjectOfType<Canvas>().gameObject.GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void SetCardData(int num, SUITS suit, GameObject dummy)
    {
        dummyObject = dummy;
        cardNumber = num;
        cardSuit = suit;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        leftCollider.SetSelected(true);
        canCreateGroup = false;
        HandCardManager.Instance.ActivateCreateGroupUI(true);
        dragPos = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        prevDragPos = dragPos;
        this.transform.position = new Vector3(eventData.position.x, eventData.position.y + 10, 0);
        dragPos = this.transform.position;
        Vector3 moveDelta = (prevDragPos - dragPos);
        prevDragPos = this.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        leftCollider.SetSelected(false);
        HandCardManager.Instance.ActivateCreateGroupUI(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dummyObject.transform.SetParent(this.transform.parent);
        dummyObject.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        leftCollider.gameObject.SetActive(true);
        this.transform.SetParent(canvas);
        dummyObject.SetActive(true);
        Color color = image.color;
        color.a = .7f;
        image.color = color;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canCreateGroup)
        {
            canCreateGroup = false;
            this.transform.SetParent(HandCardManager.Instance.AddGroup());
            this.transform.SetSiblingIndex(0);
        }
        else
        {
            this.transform.SetParent(dummyObject.transform.parent);
            this.transform.SetSiblingIndex(dummyObject.transform.GetSiblingIndex());
        }
        HandCardManager.Instance.CheckForEmptyGroups();
        dummyObject.transform.SetParent(canvas);
        dummyObject.SetActive(false);
        Color color = image.color;
        color.a = 1f;
        image.color = color;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 10, this.transform.position.z);
    }

    public void SetDummyParent(Transform parent)
    {
        dummyObject.transform.SetParent(parent);
    }

    public void SetCardIndexToFill(int index)
    {
        dummyObject.transform.SetSiblingIndex(index);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "GroupCreator")
        {
            Debug.Log("[CardView] Collinding with GroupCreator");
            canCreateGroup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "GroupCreator")
        {
            canCreateGroup = false;
            Debug.Log("[CardView] Exitting Collider with GroupCreator");
        }
    }


}
