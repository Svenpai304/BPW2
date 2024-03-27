using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Color activeColor;
    public Color inactiveColor;

    [HideInInspector] public PlayerActions playerActions;
    public List<UI_ItemSlot> slots = new List<UI_ItemSlot>();
    public List<Image> slotImages = new List<Image>();
    public UI_Item UI_ItemPrefab;
    public Item grabItem;

    public int weaponSlots = 3;
    void Awake()
    {
        GetComponentsInChildren<UI_ItemSlot>(slots);
        foreach(UI_ItemSlot slot in slots)
        {
            slotImages.Add(slot.GetComponent<Image>());
        }
    }

    public void SetUseSlot(int slot)
    {
        if (playerActions != null)
        {
            playerActions.SetActiveSlot(slot);
            if (playerActions.playerTurn)
            {
                playerActions.UseItem();
            }
        }
    }

    public void SetActiveSlot(UI_ItemSlot slot)
    {
        for (int i = 0; i < weaponSlots; i++)
        {
            slotImages[i].color = inactiveColor;
            if(slotImages[i] == slot.GetComponent<Image>())
            {
                slotImages[i].color = activeColor;
            }
        }
       
    }

    public void PickupItem(Item item)
    {
        UI_ItemSlot emptySlot = null;
        switch (item.itemType)
        {
            case Item.ItemType.Active:
                emptySlot = slots.Find(x => x.heldItem == null && slots.IndexOf(x) <= weaponSlots - 1); break;
            case Item.ItemType.Passive:
                emptySlot = slots.Find(x => x.heldItem == null && slots.IndexOf(x) > weaponSlots - 1); break;
        }
        if (emptySlot != null) 
        {
            UI_Item uiItem = Instantiate(UI_ItemPrefab);
            uiItem.Setup(item);
            emptySlot.GetItem(uiItem);
        }
    }
}
