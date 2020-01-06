using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {

        if (HandCardManager.Instance.selectedCard != null)
        {
            HandCardManager.Instance.MoveSelectedCard(eventData.position);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //Debug.Log("OnPointerDown " + eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>() != null)
            {
                HandCardManager.Instance.SelectCard(eventData.pointerCurrentRaycast.gameObject.GetComponent<CardView>());
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HandCardManager.Instance.OnCardRelease();
    }
}

