using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHeal : OnHealEffect
{
    public override int OnHeal(int heal)
    {
        return heal * 2;
    }


}
