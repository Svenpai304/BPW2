using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeStrike : EnemyAttack
{
    public int damage;
    public float duration;

    private void Start()
    {
        transform.Translate((controller.playerPosition - controller.transform.position).normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStatus ps = other.GetComponent<PlayerStatus>();
        if (ps != null)
        {
            ps.TakeDamage(damage);
        }
    }

    public override void TakeTurn()
    {
        base.TakeTurn();
    }

    public override void EndTurn()
    {
        duration -= 1;
        if (duration <= 0)
        {
            turnController.enemyAttacks.Remove(this);
            Destroy(gameObject);
        }
    }
}
