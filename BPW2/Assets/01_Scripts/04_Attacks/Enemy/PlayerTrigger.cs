using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public EnemyAttack leader;
    public PlayerStatus player;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerStatus>();
        if (player != null)
            leader.HitPlayer(player);
    }
}
