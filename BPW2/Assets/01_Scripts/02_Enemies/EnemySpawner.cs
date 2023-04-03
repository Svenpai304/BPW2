using SimpleDungeon;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]public List<Enemy> enemyOptions;

    public int basePoints;
    public int roomIndex;
    public int enemyPoints;

    public TurnController turnController;
    public List<GameObject> enemySpawns;
    public List<EnemyController> doneEnemySpawns;
    public Vector3 playerPosition;
    public DungeonGenerator dungeon;
    public bool activated = false;


    public int minX = -3;
    public int maxX = 3;
    public int minZ = -2;
    public int maxZ = 2;

    private void Start()
    {
        enemyPoints = basePoints + roomIndex;
        SelectEnemySpawns();
    }

    public void SelectEnemySpawns()
    {
        int lowestCost = 1000;
        for(int i = 0; i < enemyOptions.Count; i++)
        {
            if(enemyOptions[i].cost < lowestCost)
            lowestCost = enemyOptions[i].cost;
        }
        for (int i = enemyPoints; i >= lowestCost; i += 0)
        {
            int enemyChosen = UnityEngine.Random.Range(0, enemyOptions.Count);
            if(enemyOptions[enemyChosen].cost <= i)
            {
                enemySpawns.Add(enemyOptions[enemyChosen].enemyObject);
                i -= enemyOptions[enemyChosen].cost;
            }
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            Vector3Int position = Vector3Int.zero;
            position.x = (int)transform.position.x + UnityEngine.Random.Range(minX, maxX);
            position.z = (int)transform.position.z + UnityEngine.Random.Range(minZ, maxZ);
            if (dungeon.dungeon.ContainsKey(position)) 
            {
                if (dungeon.IsTileWalkable(position))
                {
                    doneEnemySpawns.Add(Instantiate(enemySpawns[i]).GetComponent<EnemyController>());
                    doneEnemySpawns[i].transform.position = position;
                    doneEnemySpawns[i].playerPosition = playerPosition;
                    doneEnemySpawns[i].dungeon = dungeon;
                    turnController.enemyControllers.Add(doneEnemySpawns[i]);
                }
                else
                {
                    i--;
                }
            }
            else
            {
                i--;
            }

        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        doneEnemySpawns.Remove(enemy);
        turnController.enemyControllers.Remove(enemy);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!activated && other.GetComponent<PlayerActions>() != null)
        {
            playerPosition = other.transform.position;
            activated = true;
            SpawnEnemies();
        }
    }

}

    [Serializable]
    public class Enemy
    {
        public Enemy(GameObject _enemyObject, int _cost)
        {
            enemyObject = _enemyObject;
            cost = _cost;
        }
        public GameObject enemyObject;
        public int cost;
    }
