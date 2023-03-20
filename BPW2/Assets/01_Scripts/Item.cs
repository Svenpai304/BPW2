using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event System.Action<Item> OnItemUse;

    public Sprite icon;
    public virtual void ItemUse()
    {
        OnItemUse?.Invoke(this);
    }
}
