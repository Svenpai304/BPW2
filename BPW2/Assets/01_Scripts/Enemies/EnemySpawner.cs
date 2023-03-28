using SimpleDungeon;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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

    }

    private void OnTriggerStay(Collider other)
    {
        if(!activated && other.GetComponent<PlayerActions>() != null)
        {
            playerPosition = other.transform.position;
            activated = true;
            SpawnEnemies();
        }
    }
    private void Update()
    {
        if (activated && doneEnemySpawns.Count == 0)
        {
                EndRoom();
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawns.Count; i++)
        {
            doneEnemySpawns.Add(Instantiate(enemySpawns[i]).GetComponent<EnemyController>());
            Vector3 position = Vector3.zero;
            position.x = transform.position.x + Random.Range(minX, maxX);
            position.z = transform.position.z + Random.Range(minZ, maxZ);
            doneEnemySpawns[i].transform.position = position;
            doneEnemySpawns[i].playerPosition = playerPosition;
            doneEnemySpawns[i].dungeon = dungeon;
            turnController.enemyControllers.Add(doneEnemySpawns[i]);
        }
    }

    public void EndRoom()
    {

    }
}
