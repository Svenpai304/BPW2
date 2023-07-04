using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyController controller;
    public TurnController turnController;

    public virtual void TakeTurn()
    {

    }

    public virtual void EndTurn()
    {

    }

    public virtual void DestroyProjectile()
    {
        turnController.enemyAttacks.Remove(this);
        Destroy(gameObject);
    }

    public virtual void HitPlayer(PlayerStatus player)
    {

    }
}
