using SimpleDungeon;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public int currentCooldown;
    public GameObject attackObject;

    public DungeonGenerator dungeon;
    public EnemySpawner spawner;
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
        spawner.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
