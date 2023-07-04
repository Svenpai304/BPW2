using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerStatus status;
    public PlayerActions actions;
    public TurnController turnController;

    public virtual void Awake()
    {
        status = FindObjectOfType<PlayerStatus>();
        actions = status.GetComponent<PlayerActions>();
        turnController = actions.turnController;
        turnController.playerAttacks.Add(this);
    }

    public virtual void TakeTurn()
    {

    }

    public virtual void EndTurn()
    {

    }

    public virtual void DestroyProjectile()
    {
        turnController.playerAttacks.Remove(this);
        Destroy(gameObject);
    }

    public virtual void HitEnemy(EnemyController enemyController)
    {

    }
}
