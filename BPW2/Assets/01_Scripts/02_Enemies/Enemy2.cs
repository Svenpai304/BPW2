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
            if (Vector3.Distance(transform.position, playerPosition) < attackDistance || CheckAxisAligned() > 1)
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
        base.Move();
        Vector3 chosenOption = Vector3.zero;
        float optionDistance = 0;
        float optionAxisDistance = Mathf.Infinity;
        for (int i = 0; i < movementOptions.Length; i++)
        {
            Vector3 option = movementOptions[i] + transform.position;
            Vector3Int optionTile = new Vector3Int((int)option.x, 0, (int)option.z);
            float xDistance = Mathf.Abs(option.x - playerPosition.x);
            float zDistance = Mathf.Abs(option.z - playerPosition.z);
            float axisDistance = xDistance;
            if (zDistance < axisDistance) 
            { 
                axisDistance = zDistance; 
            }
            RaycastHit[] hits = Physics.RaycastAll(option + Vector3.up * 5, Vector3.down, 10);
            bool tileClear = true;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null)
                {
                    if ((hit.collider.GetComponent<EnemyController>() != null && hit.collider.gameObject != this.gameObject) || hit.collider.GetComponent<PlayerActions>() != null)  { tileClear = false; }
                }
            }

            if (dungeon.IsTileWalkable(optionTile) && tileClear)
            {
                if (axisDistance <= optionAxisDistance)
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

    public float CheckAxisAligned()
    {
        float xDistance = Mathf.Abs(transform.position.x - playerPosition.x);
        float zDistance = Mathf.Abs(transform.position.z - playerPosition.z);
        float axisDistance = xDistance;
        if (zDistance < axisDistance)
        {
            axisDistance = zDistance;
        }
        return axisDistance;
    }
}
