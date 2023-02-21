
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace SimpleDungeon
{
    public enum TileType { Floor, Wall }
    public class DungeonGenerator : MonoBehaviour
    {
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        public int gridWidth = 200;
        public int gridHeight = 200;
        public int minRoomSize = 3;
        public int maxRoomSize = 9;
        public int numRooms = 10;
        public int startRoomMinOffset = 20;
        public int startRoomMaxOffset = 40;
        public int bossPathMinLength = 3;
        public int bossPathMaxLength = 5;
        public int lootPathMinLength = 3;
        public int lootPathMaxLength = 5;
        public int secretPathMinLength = 3;
        public int secretPathMaxLength = 5;
        private int bossPathLength = 0;
        private int lootPathLength = 0;
        private int secretPathLength = 0;
        private int debugCounter = 0;
        public Vector2 startPoint;
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
            //Rooms allocaten
            //Connect Rooms with corridors
            //Generate the dungeon
            //Doors?
            //Remove double corridors
            //Add Enemies
            //Add loot
            //Add Player
            ClearDungeon();
            GenerateStart();
            AllocatePathRooms();
            //ConnectRooms();
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
            int startSide = Random.Range(1, 4);
            switch (startSide)
            {
                case 1:
                    startX = Random.Range(startRoomMinOffset, gridWidth - startRoomMinOffset);
                    startY = Random.Range(gridHeight - startRoomMaxOffset, gridHeight - startRoomMinOffset); break;
                case 2:
                    startX = Random.Range(gridWidth - startRoomMaxOffset, gridWidth - startRoomMinOffset);
                    startY = Random.Range(startRoomMinOffset, gridHeight - startRoomMinOffset); break;
                case 3:
                    startX = Random.Range(startRoomMinOffset, gridWidth - startRoomMinOffset);
                    startY = Random.Range(startRoomMinOffset, startRoomMaxOffset); break;
                case 4:
                    startX = Random.Range(startRoomMinOffset, startRoomMaxOffset);
                    startY = Random.Range(startRoomMinOffset, gridHeight - startRoomMinOffset); break;
            }
            startPoint = new Vector2(startX, startY);
            Room startRoom = new Room(startX - 3, startX + 3, startY - 3, startY + 3);
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
            for (int i = 1; i < bossPathLength; i++)
            { 
                int dirX = Random.Range(0, 2) * 2 - 1;
                int dirZ = Random.Range(0, 2) * 2 - 1;
                int minX = Random.Range(bossRoomList[i - 1].minX + 20, bossRoomList[i - 1].minX + 25) * dirX;
                int maxX = minX + Random.Range(minRoomSize, maxRoomSize) * dirX;
                int minZ = Random.Range(bossRoomList[i - 1].minZ + 20, bossRoomList[i - 1].minZ + 25) * dirZ;
                int maxZ = minZ + Random.Range(minRoomSize, maxRoomSize) * dirZ;
                if(minX > maxX)
                {
                    int swapX = minX;
                    minX = maxX;
                    maxX = swapX;
                }
                if(minZ > maxZ)
                {
                    int swapZ = minZ;
                    minZ = maxZ;
                    maxZ = swapZ;
                }
                Room room = new Room(minX, maxX, minZ, maxZ);
                if (CanRoomFitInDungeon(room))
                {
                    AddRoomToDungeon(room);
                    bossRoomList.Add(room);
                    debugCounter = 0;
                }
                else
                {
                    i--;
                    debugCounter++;
                }
                if(debugCounter > 40)
                {
                    Debug.Log("Failed to generate path");
                    return;
                }
            }
        }

        private void ConnectRooms()
        {
            // [0, 1, 2, {3}, 4, 5] 0, 1, 2
            for (int i = 0; i < roomList.Count; i++)
            {
                Room room = roomList[i];
                Room otherRoom = roomList[(i + Random.Range(1, roomList.Count)) % roomList.Count];
                ConnectRooms(room, otherRoom);
            }
        }
        private void AllocateRooms()
        {
            for (int i = 0; i < numRooms; i++)
            {
                int minX = Random.Range(0, gridWidth);
                int maxX = minX + Random.Range(minRoomSize, maxRoomSize + 1);
                int minZ = Random.Range(0, gridHeight);
                int maxZ = minZ + Random.Range(minRoomSize, maxRoomSize + 1);
                Room room = new Room(minX, maxX, minZ, maxZ);
                if (CanRoomFitInDungeon(room))
                {
                    AddRoomToDungeon(room);
                }
                else
                {
                    i--;
                }
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
