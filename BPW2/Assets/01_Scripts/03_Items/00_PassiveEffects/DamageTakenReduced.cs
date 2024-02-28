using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenReduced : OnDamageEffect
{
    public float multiplier;

    public override int OnDamage(int damage)
    {
        return (int)(damage * multiplier);
    }
}
