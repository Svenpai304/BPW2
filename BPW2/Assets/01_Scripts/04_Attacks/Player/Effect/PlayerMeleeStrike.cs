using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMeleeStrike : PlayerAttack
{
    public int damage;
    public float duration;
    public override void Awake()
    {
        base.Awake();
        transform.Translate(actions.transform.position + actions.direction.normalized);
        transform.Rotate(new Vector3(0, Vector3.SignedAngle(Vector3.forward, actions.direction, Vector3.up), 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
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
            turnController.playerAttacks.Remove(this);
            Destroy(gameObject);
        }
    }
}