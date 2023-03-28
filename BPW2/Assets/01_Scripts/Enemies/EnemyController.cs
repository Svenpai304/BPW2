using SimpleDungeon;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public GameObject attackObject;

    public DungeonGenerator dungeon;
    public Vector3 playerPosition;

    public virtual void Update()
    {
        if(health < 0) 
        { 
            Die();
        }
    }

    public virtual void TakeTurn(Vector3 _playerPosition)
    {
        playerPosition = _playerPosition;
    }

    public virtual void Move()
    {

    }

    public virtual void Attack()
    {
        GameObject attack = Instantiate(attackObject);
        attack.transform.position = transform.position;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
