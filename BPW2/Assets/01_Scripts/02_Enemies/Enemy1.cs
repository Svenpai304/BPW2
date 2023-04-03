using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy1 : EnemyController
{
    public int attackRange = 1;
    public int actionCooldown = 2;
    public int movementCooldown = 1;
    public Vector3[] movementOptions = new Vector3[4] {Vector3.forward, Vector3.left, Vector3.right, Vector3.back};
    public override void TakeTurn(Vector3 _playerPosition)
    {
        playerPosition = _playerPosition;
        if (currentCooldown == 0)
        {
            if (Vector3.Distance(transform.position, playerPosition) > attackRange)
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
        else currentCooldown--;
    }

    public override void Attack()
    {
        currentCooldown = actionCooldown;
        base.Attack();
    }
    public override void Move()
    {
        Vector3 chosenOption = Vector3.zero;
        float optionDistance = Mathf.Infinity;
        for(int i = 0; i < movementOptions.Length; i++)
        {
            Vector3 option = movementOptions[i] + transform.position;
            Vector3Int optionTile = new Vector3Int((int)option.x, 0, (int)option.z);
            if(dungeon.IsTileWalkable(optionTile) && Vector3.Distance(option, playerPosition) < optionDistance)
            {
                optionDistance = Vector3.Distance(option, playerPosition);
                chosenOption = movementOptions[i];
            }
        }
        if(chosenOption != Vector3.zero)
        {
            transform.Translate(chosenOption);
            currentCooldown = movementCooldown;
        }
    }

}
