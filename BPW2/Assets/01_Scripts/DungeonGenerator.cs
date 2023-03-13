
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

namespace SimpleDungeon
{
    public enum TileType { Floor, Wall }
    public class DungeonGenerator : MonoBehaviour
    {
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        public int roomSizeX = 3;
        public int roomSizeZ = 9;
        public int roomSpacingX = 16;
        public int roomSpacingZ = 16;
        public int startRoomSizeX = 10;
        public int startRoomSizeZ = 10;
        public int bossPathMinLength = 3;
        public int bossPathMaxLength = 5;
        public int lootPathMinLength = 3;
        public int lootPathMaxLength = 5;
        public int secretPathMinLength = 3;
        public int secretPathMaxLength = 5;
        private int bossPathLength = 0;
        private int lootPathLength = 0;
        private int secretPathLength = 0;
        public Dictionary<Vector3Int, TileType> dungeon = new Dictionary<Vector3Int, TileType>();
        public List<Room> roomList = new List<Room>();
        public List<Room> bossRoomList = new List<Room>();
        public List<Room> lootRoomList = new List<Room>();
        public List<Room> secretRoomList = new List<Room>();
        public List<GameObject> allInstantiatedPrefabs = new List<GameObject>();
        void Start()
        {
            Generate();
        }
        /// <summary>
        /// Generates the dungeon
        /// </summary>
        [ContextMenu("Generate Dungeon")]
        public void Generate()
        {
            Debug.Log("Start Generating");
            ClearDungeon();
            GenerateStart();
            AllocatePathRooms();
            ConnectRooms();
            AllocateWalls();
            SpawnDungeon();
        }
        [ContextMenu("Clear Dungeon")]
        public void ClearDungeon()
        {
            for (int i = allInstantiatedPrefabs.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(allInstantiatedPrefabs[i]);
            }
            dungeon.Clear();
            roomList.Clear();
            bossRoomList.Clear();
            lootRoomList.Clear();
            secretRoomList.Clear();
        }

        private void GenerateStart()
        {
            int startX = 0;
            int startY = 0;
            Room startRoom = new Room(startX, startX + roomSizeX, startY, startY + roomSizeZ);
            AddRoomToDungeon(startRoom);
            bossRoomList.Add(startRoom);
            lootRoomList.Add(startRoom);
            secretRoomList.Add(startRoom);
            bossPathLength = Random.Range(bossPathMinLength, bossPathMaxLength);
            lootPathLength = Random.Range(lootPathMinLength, lootPathMaxLength);
            secretPathLength = Random.Range(secretPathMinLength, secretPathMaxLength);
        }

        private void AllocatePathRooms()
        {
            int roomRetryCounter = 0;
            int pathRetryCounter = 0;

            bool lootPathSplit = false;
            bool secretPathSplit = false;
            for (int i = 1; i < bossPathLength; i++)
            {
                int bossDirX = Random.Range(0, 2);
                int bossDirZ = 0;

                if (bossDirX == 0)
                {
                    bossDirZ = Random.Range(0, 2) * 2 - 1;
                }
                if (bossDirX == 1)
                {
                    bossDirX *= Random.Range(0, 2) * 2 - 1;
                }

                int minX = bossRoomList[i - 1].minX + roomSpacingX * bossDirX;
                int maxX = minX + roomSizeX;
                int minZ = bossRoomList[i - 1].minZ + roomSpacingZ * bossDirZ;
                int maxZ = minZ + roomSizeZ;
                Room room = new Room(minX, maxX, minZ, maxZ);
                if (CanRoomFitInDungeon(room))
                {
                    AddRoomToDungeon(room);
                    bossRoomList.Add(room);
                    roomRetryCounter = 0;
                }
                else
                {
                    i--;
                    roomRetryCounter++;
                }
                if (roomRetryCounter > 40)
                {
                    ClearPath(bossRoomList);
                    i = 0;
                    roomRetryCounter = 0;
                }
                if (pathRetryCounter > 10)
                {
                    Debug.Log("Failed to generate boss path");
                    return;
                }
            }
            for (int i = 1; i < lootPathLength; i++)
            {
                int rng = Random.Range(1, 4);
                if (rng == 3 || lootPathSplit || i >= bossPathLength - Mathf.Abs(bossPathLength - lootPathLength))
                {
                    lootPathSplit = true;
                    int dirX = Random.Range(0, 2);
                    int dirZ = 0;
                    if (dirX == 0)
                    {
                        dirZ = Random.Range(0, 2) * 2 - 1;
                    }
                    if (dirX == 1)
                    {
                        dirX *= Random.Range(0, 2) * 2 - 1;
                    }
                    int minX = lootRoomList[i - 1].minX + roomSpacingX * dirX;
                    int maxX = minX + roomSizeX;
                    int minZ = lootRoomList[i - 1].minZ + roomSpacingZ * dirZ;
                    int maxZ = minZ + roomSizeZ;
                    Room room = new Room(minX, maxX, minZ, maxZ);
                    if (CanRoomFitInDungeon(room))
                    {
                        AddRoomToDungeon(room);
                        lootRoomList.Add(room);
                        roomRetryCounter = 0;
                    }
                    else
                    {
                        i--;
                        roomRetryCounter++;
                    }
                    if (roomRetryCounter > 40)
                    {
                        ClearPath(lootRoomList);
                        i = 0;
                        roomRetryCounter = 0;
                        pathRetryCounter++;
                    }
                    if(pathRetryCounter > 10)
                    {
                        Debug.Log("Failed to generate loot path");
                        return;
                    }
                }
                else
                {
                    lootRoomList.Add(bossRoomList[i]);
                    lootPathLength++;
                }
            }
            for (int i = 1; i < secretPathLength; i++)
            {
                int rng = Random.Range(1, 4);
                if (rng == 3 || secretPathSplit || i >= lootPathLength - Mathf.Abs(lootPathLength - secretPathLength))
                {
                    secretPathSplit = true;
                    int dirX = Random.Range(0, 2);
                    int dirZ = 0;
                    if (dirX == 0)
                    {
                        dirZ = Random.Range(0, 2) * 2 - 1;
                    }
                    if (dirX == 1)
                    {
                        dirX *= Random.Range(0, 2) * 2 - 1;
                    }
                    int minX = secretRoomList[i - 1].minX + roomSpacingX * dirX;
                    int maxX = minX + roomSizeX;
                    int minZ = secretRoomList[i - 1].minZ + roomSpacingZ * dirZ;
                    int maxZ = minZ + roomSizeZ;
                    Room room = new Room(minX, maxX, minZ, maxZ);
                    if (CanRoomFitInDungeon(room))
                    {
                        AddRoomToDungeon(room);
                        secretRoomList.Add(room);
                        roomRetryCounter = 0;
                    }
                    else
                    {
                        i--;
                        roomRetryCounter++;
                    }
                    if (roomRetryCounter > 40)
                    {
                        ClearPath(secretRoomList);
                        i = 0;
                        roomRetryCounter = 0;

                    }
                    if (pathRetryCounter > 10)
                    {
                        Debug.Log("Failed to generate secret path");
                        return;
                    }
                }
                else
                {
                    secretRoomList.Add(lootRoomList[i]);
                    secretPathLength++;
                }
            }
            RemoveRoomFromDungeon(bossRoomList[0]);
            int startRoomOffsetX = (roomSizeX - startRoomSizeX) / 2;
            int startRoomOffsetZ = (roomSizeZ - startRoomSizeZ) / 2;
            Room trueStartRoom = new Room(startRoomOffsetX, startRoomOffsetX + startRoomSizeX, startRoomOffsetZ, startRoomOffsetZ + startRoomSizeZ);
            roomList[0] = trueStartRoom;
            bossRoomList[0] = trueStartRoom;
            lootRoomList[0] = trueStartRoom;
            secretRoomList[0] = trueStartRoom;
            AddRoomToDungeon(trueStartRoom);
        }

        private void ClearPath(List<Room> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list != bossRoomList && !bossRoomList.Contains(list[i]))
                {
                    RemoveRoomFromDungeon(list[i]);
                    roomList.Remove(list[i]);
                }
                if (list != lootRoomList && !lootRoomList.Contains(list[i]))
                {
                    RemoveRoomFromDungeon(list[i]);
                    roomList.Remove(list[i]);
                }
            }
            list.Clear();
            list.Add(roomList[0]);
        }

        private void ConnectRooms()
        {
            for (int i = 1; i < bossRoomList.Count; i++)
            {
                Room room = bossRoomList[i];
                Room otherRoom = bossRoomList[i - 1];
                ConnectRooms(room, otherRoom);
            }
            for (int i = 1; i < lootRoomList.Count; i++)
            {
                Room room = lootRoomList[i];
                Room otherRoom = lootRoomList[i - 1];
                ConnectRooms(room, otherRoom);
            }
            for (int i = 1; i < secretRoomList.Count; i++)
            {
                Room room = secretRoomList[i];
                Room otherRoom = secretRoomList[i - 1];
                ConnectRooms(room, otherRoom);
            }
            
        }
        
        public void AllocateWalls()
        {
            var keys = dungeon.Keys.ToList();
            foreach (var kv in keys)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        //if(Mathf.Abs(x) == Mathf.Abs(z)) { continue; }
                        Vector3Int newPos = kv + new Vector3Int(x, 0, z);
                        if (dungeon.ContainsKey(newPos)) { continue; }
                        dungeon.Add(newPos, TileType.Wall);
                    }
                }
            }
        }
        public void ConnectRooms(Room _roomOne, Room _roomTwo)
        {
            Vector3Int posOne = _roomOne.GetCenter();
            Vector3Int posTwo = _roomTwo.GetCenter();
            int dirX = posTwo.x > posOne.x ? 1 : -1;
            int x = 0;
            for (x = posOne.x; x != posTwo.x; x += dirX)
            {
                Vector3Int position = new Vector3Int(x, 0, posOne.z);
                if (dungeon.ContainsKey(position)) { continue; }
                dungeon.Add(position, TileType.Floor);
            }
            int dirZ = posTwo.z > posOne.z ? 1 : -1;
            for (int z = posOne.z; z != posTwo.z; z += dirZ)
            {
                Vector3Int position = new Vector3Int(x, 0, z);
                if (dungeon.ContainsKey(position)) { continue; }
                dungeon.Add(position, TileType.Floor);
            }
        }
        public void SpawnDungeon()
        {
            foreach (KeyValuePair<Vector3Int, TileType> kv in dungeon)
            {
                GameObject obj = null;
                switch (kv.Value)
                {
                    case TileType.Floor:
                        obj = Instantiate(floorPrefab, kv.Key, Quaternion.identity, transform); break;
                    case TileType.Wall:
                        obj = Instantiate(wallPrefab, kv.Key, Quaternion.identity, transform); break;
                }
                allInstantiatedPrefabs.Add(obj);
            }
        }
        public void AddRoomToDungeon(Room room)
        {
            for (int x = room.minX; x <= room.maxX; x++)
            {
                for (int z = room.minZ; z <= room.maxZ; z++)
                {
                    dungeon.Add(new Vector3Int(x, 0, z), TileType.Floor);
                }
            }
            roomList.Add(room);
        }

        public void RemoveRoomFromDungeon(Room room)
        {
            for (int x = room.minX; x <= room.maxX; x++)
            {
                for (int z = room.minZ; z <= room.maxZ; z++)
                {
                    dungeon.Remove(new Vector3Int(x, 0, z));
                }
            }
        }

        public bool CanRoomFitInDungeon(Room room)
        {
            for (int x = room.minX - 1; x <= room.maxX + 1; x++)
            {
                for (int z = room.minZ - 1; z <= room.maxZ + 1; z++)
                {
                    if (dungeon.ContainsKey(new Vector3Int(x, 0, z)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    public class Room
    {
        public int minX, maxX, minZ, maxZ;
        public Room(int _minX, int _maxX, int _minZ, int _maxZ)
        {
            minX = _minX;
            maxX = _maxX;
            minZ = _minZ;
            maxZ = _maxZ;
        }
        public Vector3Int GetCenter()
        {
            return new Vector3Int(Mathf.RoundToInt(Mathf.Lerp(minX, maxX, 0.5f)), 0, Mathf.RoundToInt(Mathf.Lerp(minZ, maxZ, 0.5f)));
        }
        public Vector3Int GetRandomPositionInRoom()
        {
            return new Vector3Int(Random.Range(minX, maxX + 1), 0, Random.Range(minZ, maxZ + 1));
        }
    }
}
