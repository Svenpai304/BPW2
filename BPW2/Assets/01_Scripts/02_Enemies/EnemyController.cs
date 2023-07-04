using SimpleDungeon;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int currentCooldown;
    public GameObject attackObject;

    public DungeonGenerator dungeon;
    public EnemySpawner spawner;
    public TurnController turnController;
    public Vector3 playerPosition;

    public virtual void Start()
    {
        health = maxHealth;
    }

    public virtual void Update()
    {
        if(health <= 0) 
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
        if (attackObject == null)
            return;
        GameObject attack = Instantiate(attackObject);
        attack.transform.position = transform.position;
        EnemyAttack ea = attack.GetComponent<EnemyAttack>();
        if (ea != null)
        {
            ea.controller = this;
            ea.turnController = turnController;
            turnController.enemyAttacks.Add(ea);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Mathf.Clamp(health, 0, maxHealth);
    }

    public virtual void Die()
    {
        spawner.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
