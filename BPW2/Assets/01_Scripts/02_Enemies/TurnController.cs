using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public float playerTurnSeconds;
    public float enemyTurnSeconds;
    public PlayerActions playerActions;
    public List<PlayerAttack> playerAttacks;
    public List<EnemyController> enemyControllers;
    public List<EnemyAttack> enemyAttacks;
    public enum Turn { Player, PlayerActive, Enemy, EnemyActive };
    public Turn currentTurn;
    private void Start()
    {
        currentTurn = Turn.Player;
    }

    private void Update()
    {
        if(currentTurn == Turn.Enemy)
        {
            currentTurn = Turn.EnemyActive;
            playerActions.playerTurn = false;
            StartCoroutine(EnemyTurn());
        }
        if(currentTurn == Turn.Player)
        {
            currentTurn = Turn.PlayerActive;
            playerActions.playerTurn = true;
        }
    }

    public IEnumerator EnemyTurn()
    {
        if(playerAttacks.Count > 0)
        {
            for(int i = 0; i < playerAttacks.Count; i++)
            {
                playerAttacks[i].TakeTurn();
            }
            yield return new WaitForSeconds(playerTurnSeconds);
            for (int i = 0; i < playerAttacks.Count; i++)
            {
                playerAttacks[i].EndTurn();
            }
        }
        if (enemyControllers.Count > 0 || enemyAttacks.Count > 0)
        {
            yield return new WaitForSeconds(playerTurnSeconds);
            for (int i = 0; i < enemyControllers.Count; i++)
            {
                enemyControllers[i].TakeTurn(playerActions.transform.position);
            }
            for(int i = 0; i < enemyAttacks.Count; i++)
            {
                enemyAttacks[i].TakeTurn();
            }

            yield return new WaitForSeconds(enemyTurnSeconds);

            for (int i = 0; i < enemyAttacks.Count; i++)
            {
                enemyAttacks[i].EndTurn();
            }
        }
        currentTurn = Turn.Player;
    }
}
