using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamageEffect : MonoBehaviour
{
    
    public virtual int OnDamage(int damage)
    {
        return damage;
    }
}
