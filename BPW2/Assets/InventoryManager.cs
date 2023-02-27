using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<UI_ItemSlot> slots = new List<UI_ItemSlot>();
    public UI_Item UI_ItemPrefab;
    public event System.Action OnGrab;
    void Awake()
    {
        GetComponentsInChildren<UI_ItemSlot>(slots);
        OnGrab += GrabItem;
    }

    public void GrabItem()
    {
        Item item = FindObjectOfType<Item>();
        PickupItem(item);
    }

    public void PickupItem(Item item)
    {
        UI_ItemSlot emptySlot = slots.Find(x => x.heldItem == null);
        if (emptySlot != null) 
        {
            UI_Item uiItem = Instantiate(UI_ItemPrefab);
            uiItem.Setup(item);
            emptySlot.GetItem(uiItem);
        }
    }
}
