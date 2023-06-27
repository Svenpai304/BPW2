using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitHandler : MonoBehaviour
{
    public PlayerActions pa;
    public PlayerStatus ps;

    public void OnHit(int damage)
    {
        if (ps != null) 
        {
            ps.TakeDamage(damage);
        }
    }

    public void OnHeal(int heal)
    {
        if (ps != null)
        {
            ps.HealDamage(heal);
        }
    }
}
