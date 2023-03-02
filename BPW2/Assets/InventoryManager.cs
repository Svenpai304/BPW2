using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InventoryManager : MonoBehaviour
{
    public List<UI_ItemSlot> slots = new List<UI_ItemSlot>();
    public UI_Item UI_ItemPrefab;
    public Item grabItem;
    void Awake()
    {
        GetComponentsInChildren<UI_ItemSlot>(slots);
    }

    [ContextMenu("Grab item")] 
    public void GrabItem()
    {
        Item item = grabItem;
        Debug.Log("Pickup");
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
