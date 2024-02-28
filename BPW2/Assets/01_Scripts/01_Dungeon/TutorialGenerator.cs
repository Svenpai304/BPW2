using SimpleDungeon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialGenerator : DungeonGenerator
{

    public override void Generate()
    {
        Debug.Log("Start Generating");
        ClearDungeon();
        allInstantiatedPrefabs.Clear();
        GenerateStart();
        AllocateSinglePath(bossRoomList, null, bossPathMaxLength, 0);
        ConnectRooms();
        AllocateWalls();
        SpawnPlayer();
        SpawnDungeon();
    }

    public override void AllocateSinglePath(List<Room> mainList, List<Room> previousList, int pathLength, int previousPathLength)
    {
        for (int i = 1; i < pathLength; i++)
        {
            int dirX = 1;
            int dirZ = 0;
            int minX = mainList[i - 1].minX + roomSpacingX * dirX;
            int maxX = minX + roomSizeX;
            int minZ = mainList[i - 1].minZ + roomSpacingZ * dirZ;
            int maxZ = minZ + roomSizeZ;

            Type roomType = typeof(Room);
            if(i == 2)
            {
                roomType = typeof(SpikeCrossRoom);
            }
            Room room = Activator.CreateInstance(roomType, minX, maxX, minZ, maxZ, i) as Room;

            if (CanRoomFitInDungeon(room))
            {
                AddRoomToDungeon(room);
                mainList.Add(room);
                continue;
            }
        }
        RemoveRoomFromDungeon(bossRoomList[0]);
        int startRoomOffsetX = (roomSizeX - startRoomSizeX) / 2;
        int startRoomOffsetZ = (roomSizeZ - startRoomSizeZ) / 2;
        StartRoom trueStartRoom = new StartRoom(startRoomOffsetX, startRoomOffsetX + startRoomSizeX, startRoomOffsetZ, startRoomOffsetZ + startRoomSizeZ, 0);
        roomList[0] = trueStartRoom;
        bossRoomList[0] = trueStartRoom;
        AddRoomToDungeon(trueStartRoom);
        Room chosenRoom = bossRoomList[bossRoomList.Count - 1];
        RemoveRoomFromDungeon(chosenRoom);
        bossRoomList[bossRoomList.Count - 1] = new BossRoom(chosenRoom.minX, chosenRoom.maxX, chosenRoom.minZ, chosenRoom.maxZ, chosenRoom.index);
        AddRoomToDungeon(bossRoomList[bossRoomList.Count - 1]);
    }

    public override void AddRoomToDungeon(Room room)
    {
        room.SpawnTiles(this);
        if (standardRooms.Contains(room.GetType()))
        {
            if (room.GetType() == typeof(BossRoom))
            {
                GameObject obj = Instantiate(spawnerPrefab, room.GetCenter(), Quaternion.identity, transform);
                EnemySpawner es = obj.GetComponent<EnemySpawner>();

                es.dungeon = this;
                es.turnController = turnController;
                es.roomIndex = room.index;
                allInstantiatedPrefabs.Add(obj);
            }
        }
        roomList.Add(room);
    }
}
