using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHealEffect : MonoBehaviour
{
    public virtual int OnHeal(int heal)
    {
        return heal;
    }
}
