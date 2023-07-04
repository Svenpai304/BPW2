using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public event System.Action<Item> OnItemUse;
    public UnityEvent effects;
    public enum ItemType { Active, Passive }
    public enum ItemUseType { None, Orthogonal, Self }
    public ItemType itemType;
    public ItemUseType itemUseType;
    
    public string hoverName;
    public string hoverText;

    public Sprite icon;
    [HideInInspector] public UI_Item uiItem;
    public virtual void ItemUse()
    {
        OnItemUse?.Invoke(this);
        effects?.Invoke();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        PlayerActions pa = other.GetComponent<PlayerActions>();
        if(pa != null)
        {
            pa.inventoryManager.PickupItem(this);
        }
    }
}
