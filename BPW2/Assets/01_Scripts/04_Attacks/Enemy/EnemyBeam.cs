using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyBeam : EnemyAttack
{
    public int damage;
    public float duration;
    public float turnDistance;
    private Vector3 direction;

    public Transform turnPoint;

    private void Start()
    {
        direction = (controller.playerPosition - controller.transform.position).normalized;
        direction = new Vector3Int((int)Mathf.Round(direction.x), (int)Mathf.Round(direction.y), (int)Mathf.Round(direction.z));
        if(Mathf.Abs(direction.z) == Mathf.Abs(direction.x))
        {
            if(UnityEngine.Random.Range(0, 2)  == 0 )
            {
                direction.x = 0;
            }
            else
            {
                direction.z = 0;
            }
        }
        transform.Translate(direction);
        Vector3 rotation = new Vector3(0, Vector3.SignedAngle(Vector3.forward, direction, Vector3.up), 0);
        turnPoint.Rotate(rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStatus player = other.GetComponent<PlayerStatus>();
        if (player != null)
        {
            HitPlayer(player);
        }
    }

    public override void HitPlayer(PlayerStatus player)
    {
        if (player != null) 
        {
            player.TakeDamage(damage);
            DestroyProjectile();
        }
    }

    public override void TakeTurn()
    {
        transform.Translate(direction * turnDistance);
        base.TakeTurn();
    }

    public override void EndTurn()
    {
        duration -= 1;
        if (duration <= 0)
        {
            DestroyProjectile();
        }
    }
}
