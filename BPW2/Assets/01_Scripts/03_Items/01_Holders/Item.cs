using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public event System.Action<Item> OnItemUse;
    public UnityEvent effects;
    public PlayerActions playerActions;
    public PlayerStatus playerStatus;
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
    public virtual void OnItemAdd()
    {

    }
    public virtual void OnItemRelease()
    {
        playerActions = null;
        playerStatus = null;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        playerActions = other.GetComponent<PlayerActions>();
        if(playerActions != null)
        {
            playerStatus = other.GetComponent<PlayerStatus>();
            playerActions.inventoryManager.PickupItem(this);
        }
    }
}
