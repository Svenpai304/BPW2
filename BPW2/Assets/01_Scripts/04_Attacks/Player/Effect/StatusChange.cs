using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusChange : Item
{
    public int healOnUse;
    PlayerStatus status;
    public override void ItemUse()
    {
        base.ItemUse();
        status = FindObjectOfType<PlayerStatus>();
        status.HealDamage(healOnUse);
        uiItem.DestroyItem();
    }

}
