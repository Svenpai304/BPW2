using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTile : MonoBehaviour
{
    public int playerDamage;
    public int enemyDamage;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
        EnemyController enemyController = other.GetComponent<EnemyController>();
        if (playerStatus != null)
        {
            playerStatus.TakeDamage(playerDamage);
        }
        if(enemyController != null)
        {
            enemyController.TakeDamage(enemyDamage);
        }

    }
}
