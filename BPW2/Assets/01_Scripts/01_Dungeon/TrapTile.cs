using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTile : MonoBehaviour
{
    public int playerDamage;
    public int enemyDamage;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStatus playerStatus = GetComponent<PlayerStatus>();
        EnemyController enemyController = GetComponent<EnemyController>();
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
