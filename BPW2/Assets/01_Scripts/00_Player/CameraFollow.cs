using SimpleDungeon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerActions player = null;

    public float roomSizeX;
    public float roomSizeZ;

    private void Start()
    {
        DungeonGenerator dungeonGenerator = FindObjectOfType<DungeonGenerator>();
        roomSizeX = dungeonGenerator.roomSpacingX;
        roomSizeZ = dungeonGenerator.roomSpacingZ;
    }
    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerActions>();
        }
        if (player.transform.position.x > transform.position.x + roomSizeX / 2)
        {
            transform.Translate(new Vector3(roomSizeX, 0, 0));
        }
        if (player.transform.position.x < transform.position.x - roomSizeX / 2)
        {
            transform.Translate(new Vector3(-roomSizeX, 0, 0));
        }
        if (player.transform.position.z > transform.position.z + roomSizeZ / 2)
        {
            transform.Translate(new Vector3(0, roomSizeZ, 0));
        }
        if (player.transform.position.z < transform.position.z - roomSizeZ / 2)
        {
            transform.Translate(new Vector3(0, -roomSizeZ, 0));
        }
    }
}
