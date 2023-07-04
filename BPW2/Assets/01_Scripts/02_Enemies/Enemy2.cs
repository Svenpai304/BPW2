using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyController
{
    public int attackDistance = 1;
    public int actionCooldown = 2;
    public int movementCooldown = 1;
    public Vector3[] movementOptions = new Vector3[8];
    public override void TakeTurn(Vector3 _playerPosition)
    {
        playerPosition = _playerPosition;
        if (currentCooldown == 0)
        {
            if (Vector3.Distance(transform.position, playerPosition) < attackDistance || !CheckAxisAligned())
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
        float optionDistance = 0;
        float optionAxisDistance = Mathf.Infinity;
        for (int i = 0; i < movementOptions.Length; i++)
        {
            Vector3 option = movementOptions[i] + transform.position;
            Vector3Int optionTile = new Vector3Int((int)option.x, 0, (int)option.z);
            float xDistance = Mathf.Abs(option.x - playerPosition.x);
            float yDistance = Mathf.Abs(option.y - playerPosition.y);
            float axisDistance = xDistance;
            if (yDistance < axisDistance) 
            { 
                axisDistance = yDistance; 
            }

            if (dungeon.IsTileWalkable(optionTile))
            {
                if (axisDistance < optionAxisDistance || (axisDistance == optionAxisDistance && Vector3.Distance(option, playerPosition) > optionDistance))
                {
                    optionDistance = Vector3.Distance(option, playerPosition);
                    optionAxisDistance = axisDistance;
                    chosenOption = movementOptions[i];
                }
            }
        }
        if (chosenOption != Vector3.zero)
        {
            transform.Translate(chosenOption);
            currentCooldown = movementCooldown;
        }
    }

    public bool CheckAxisAligned()
    {
        if (playerPosition.x == transform.position.x || playerPosition.z == transform.position.z)
            return true;
        else return false;
    }
}
