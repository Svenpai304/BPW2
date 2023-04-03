using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public float enemyTurnSeconds;
    public PlayerActions playerActions;
    public List<EnemyController> enemyControllers;
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
            StartCoroutine(EnemyTurn());
        }
        if(currentTurn == Turn.Player)
        {
            playerActions.playerTurn = true;
            currentTurn = Turn.PlayerActive;
        }
    }

    public IEnumerator EnemyTurn()
    {
        if (enemyControllers.Count > 0)
        {
            for (int i = 0; i < enemyControllers.Count; i++)
            {
                enemyControllers[i].TakeTurn(playerActions.transform.position);
            }
            yield return new WaitForSeconds(enemyTurnSeconds);
        }
        currentTurn = Turn.Player;
    }
}
