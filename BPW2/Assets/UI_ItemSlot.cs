using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemSlot : MonoBehaviour
{
    public UI_Item heldItem;

    // Update is called once per frame
    public void ReleaseItem()
    {
        heldItem = null;
    }

    public void UseItem()
    {
        heldItem.Use();
    }

    public void GetItem(UI_Item item)
    {
        heldItem = item;
        heldItem.transform.parent = transform;
        heldItem.transform.SetAsLastSibling();
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.SetSlot(this);
    }


}
