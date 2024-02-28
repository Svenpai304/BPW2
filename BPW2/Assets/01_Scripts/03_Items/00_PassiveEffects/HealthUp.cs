using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : OnAddEffect
{
    public int extraHealth;

    private PlayerStatus playerStatus;
    public override void OnAdd()
    {
        playerStatus = FindObjectOfType<PlayerStatus>();
        playerStatus.ChangeMaxHealth(playerStatus.maxHealth + extraHealth);
    }
    public override void OnRemove()
    {
        playerStatus.ChangeMaxHealth(playerStatus.maxHealth - extraHealth);
    }
}
