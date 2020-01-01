using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupView : MonoBehaviour
{
    //int groupId;
    [SerializeField] BoxCollider2D groupColliderRight;
    // Start is called before the first frame update
    private void OnRectTransformDimensionsChange()
    {
        SetColliderPosition();
    }
    public void SetColliderPosition()
    {
        float width = gameObject.GetComponent<RectTransform>().rect.width;
        groupColliderRight.offset = new Vector2((width / 2) - 4, 0);
    }

    //public void SetGroupId(int groupId)
    //{
    //    this.groupId = groupId;
    //}
    //public int GetGroupID()
    //{
    //    return groupId;
    //}
}
