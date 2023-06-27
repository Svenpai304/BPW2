using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event System.Action<Item> OnItemUse;
    public enum ItemType { Active, Passive }
    public ItemType itemType;

    public Sprite icon;
    public virtual void ItemUse()
    {
        OnItemUse?.Invoke(this);
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
