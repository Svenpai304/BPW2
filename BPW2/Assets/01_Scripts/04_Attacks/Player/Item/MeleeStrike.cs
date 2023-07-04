using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStrike : Item
{
    public GameObject attack;

    public override void ItemUse()
    {
        base.ItemUse();
        Instantiate(attack);
    }
}
