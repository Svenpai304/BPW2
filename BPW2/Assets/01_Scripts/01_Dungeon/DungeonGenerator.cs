
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SimpleDungeon
{
    public enum TileType { Floor, Wall, Trap, Victory }
    public class DungeonGenerator : MonoBehaviour
    {
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        public GameObject trapPrefab;
        public GameObject victoryPrefab;
        public GameObject spawnerPrefab;
        public GameObject playerPrefab;
        public TurnController turnController;
        public InputManager inputManager;
        public InventoryManager inventoryManager;
        public List<Type> standardRooms = new() { typeof(Room), typeof(SpikeCircleRoom), typeof(SpikeCrossRoom) };
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
            allInstantiatedPrefabs.Clear();
            GenerateStart();
            AllocatePathRooms();
            AllocateEndRoom();
            ConnectRooms();
            AllocateWalls();
            SpawnPlayer();
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
            StartRoom startRoom = new StartRoom(startX, startX + roomSizeX, startY, startY + roomSizeZ, 0);
            AddRoomToDungeon(startRoom);
            bossRoomList.Add(startRoom);
            lootRoomList.Add(startRoom);
            secretRoomList.Add(startRoom);
            bossPathLength = UnityEngine.Random.Range(bossPathMinLength, bossPathMaxLength);
            lootPathLength = UnityEngine.Random.Range(lootPathMinLength, lootPathMaxLength);
            secretPathLength = UnityEngine.Random.Range(secretPathMinLength, secretPathMaxLength);
        }

        private void AllocatePathRooms()
        {

            AllocateSinglePath(bossRoomList, null, bossPathLength, 0);
            AllocateSinglePath(lootRoomList, bossRoomList, lootPathLength, bossPathLength);
            AllocateSinglePath(secretRoomList, lootRoomList, secretPathLength, lootPathLength);
           
            RemoveRoomFromDungeon(bossRoomList[0]);
            int startRoomOffsetX = (roomSizeX - startRoomSizeX) / 2;
            int startRoomOffsetZ = (roomSizeZ - startRoomSizeZ) / 2;
            StartRoom trueStartRoom = new StartRoom(startRoomOffsetX, startRoomOffsetX + startRoomSizeX, startRoomOffsetZ, startRoomOffsetZ + startRoomSizeZ, 0);
            roomList[0] = trueStartRoom;
            bossRoomList[0] = trueStartRoom;
            lootRoomList[0] = trueStartRoom;
            secretRoomList[0] = trueStartRoom;
            AddRoomToDungeon(trueStartRoom);
        }

        public void AllocateSinglePath(List<Room> mainList, List<Room> previousList, int pathLength, int previousPathLength)
        {
            int roomRetryCounter = 0;
            int pathRetryCounter = 0;
            bool pathSplit = false;
            for (int i = 1; i < pathLength; i++)
            {
                int rng = UnityEngine.Random.Range(1, 4);
                if (rng == 3 || pathSplit || i >= previousPathLength - 2)
                {
                    pathSplit = true;
                    int dirX = UnityEngine.Random.Range(0, 2);
                    int dirZ = 0;
                    if (dirX == 0)
                    {
                        dirZ = UnityEngine.Random.Range(0, 2) * 2 - 1;
                    }
                    if (dirX == 1)
                    {
                        dirX *= UnityEngine.Random.Range(0, 2) * 2 - 1;
                    }
                    int minX = mainList[i - 1].minX + roomSpacingX * dirX;
                    int maxX = minX + roomSizeX;
                    int minZ = mainList[i - 1].minZ + roomSpacingZ * dirZ;
                    int maxZ = minZ + roomSizeZ;

                    Type roomType = standardRooms[UnityEngine.Random.Range(0, standardRooms.Count)];
                    Room room = Activator.CreateInstance(roomType, minX, maxX, minZ, maxZ, i) as Room;


                    if (CanRoomFitInDungeon(room))
                    {
                        AddRoomToDungeon(room);
                        mainList.Add(room);
                        roomRetryCounter = 0;
                    }
                    else
                    {
                        i--;
                        roomRetryCounter++;
                    }
                    if (roomRetryCounter > 40)
                    {
                        pathRetryCounter++;
                        i = 0;
                        roomRetryCounter = 0;

                    }
                    if (pathRetryCounter > 10)
                    {
                        ClearPath(mainList);
                        Debug.Log("Failed to generate path");
                        i = 1;
                    }
                }
                else
                {
                    mainList.Add(previousList[i]);
                    pathLength++;
                }
            }
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

        private void AllocateEndRoom()
        {
            int path = UnityEngine.Random.Range(0, 3);
            List<Room> chosenList = null;
            switch (path)
            {
                case 0:
                    chosenList = bossRoomList; break;
                case 1: 
                    chosenList = lootRoomList; break;
                case 2: 
                    chosenList = secretRoomList; break;
            }
            Room chosenRoom = chosenList[chosenList.Count - 1];
            RemoveRoomFromDungeon(chosenRoom);
            chosenList[chosenList.Count - 1] = new BossRoom(chosenRoom.minX, chosenRoom.maxX, chosenRoom.minZ, chosenRoom.maxZ, chosenRoom.index);
            AddRoomToDungeon(chosenList[chosenList.Count - 1]);
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
                        obj = Instantiate(floorPrefab, floorPrefab.transform.position, Quaternion.identity, transform);
                        obj.transform.Translate(kv.Key); break;
                    case TileType.Wall:
                        obj = Instantiate(wallPrefab, wallPrefab.transform.position, Quaternion.identity, transform);
                        obj.transform.Translate(kv.Key); break;
                    case TileType.Trap:
                        obj = Instantiate(trapPrefab, trapPrefab.transform.position, Quaternion.identity, transform);
                        obj.transform.Translate(kv.Key); break;
                    case TileType.Victory:
                        obj = Instantiate(victoryPrefab, victoryPrefab.transform.position, Quaternion.identity, transform);
                        obj.transform.Translate(kv.Key); break;
                }
                allInstantiatedPrefabs.Add(obj);
            }
        }
        public void AddRoomToDungeon(Room room)
        {
            room.SpawnTiles(this); 
            if (standardRooms.Contains(room.GetType()))
            {
                GameObject obj = Instantiate(spawnerPrefab, room.GetCenter(), Quaternion.identity, transform);
                EnemySpawner es = obj.GetComponent<EnemySpawner>();
                es.dungeon = this;
                es.turnController = turnController;
                es.roomIndex = room.index;
                allInstantiatedPrefabs.Add(obj);
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

        public void SpawnPlayer()
        {
            PlayerActions pa = Instantiate(playerPrefab, roomList[0].GetCenter(), Quaternion.identity).GetComponent<PlayerActions>();
            turnController.playerActions = pa;
            inputManager.playerActions = pa;
            pa.turnController = turnController;
            pa.inventoryManager = inventoryManager;
            pa.dungeon = this;
        }

        public bool IsTileWalkable(Vector3Int tile)
        {
            if (dungeon.ContainsKey(tile))
            {
                if (dungeon[tile] == TileType.Floor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsTilePlayerWalkable(Vector3Int tile)
        {
            if (dungeon.ContainsKey(tile))
            {
                if (dungeon[tile] == TileType.Floor || dungeon[tile] == TileType.Trap)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
    [Serializable]
    public class Room
    {
        public int minX, maxX, minZ, maxZ, index;
        public Room(int _minX, int _maxX, int _minZ, int _maxZ, int _index)
        {
            minX = _minX;
            maxX = _maxX;
            minZ = _minZ;
            maxZ = _maxZ;
            index = _index;
        }

        public Room() { }

        public virtual void SpawnTiles(DungeonGenerator dungeonGenerator)
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    dungeonGenerator.dungeon.Add(new Vector3Int(x, 0, z), TileType.Floor);
                }
            }
            
        }
        public Vector3Int GetCenter()
        {
            return new Vector3Int(Mathf.RoundToInt(Mathf.Lerp(minX, maxX, 0.5f)), 0, Mathf.RoundToInt(Mathf.Lerp(minZ, maxZ, 0.5f)));
        }
        public Vector3Int GetRandomPositionInRoom()
        {
            return new Vector3Int(UnityEngine.Random.Range(minX, maxX + 1), 0, UnityEngine.Random.Range(minZ, maxZ + 1));
        }
    }

    public class RoomType
    {
        public Type type;
        public RoomType(Type _type) 
        {
            type = _type;
        }
    }

    public class StartRoom : Room
    {
        public StartRoom(int _minX, int _maxX, int _minZ, int _maxZ, int _index) : base(_minX, _maxX, _minZ, _maxZ, _index)
        {
            minX = _minX;
            maxX = _maxX;
            minZ = _minZ;
            maxZ = _maxZ;
            index = _index;
        }
        public StartRoom() { }
    }

    public class BossRoom : Room
    {
        public BossRoom(int _minX, int _maxX, int _minZ, int _maxZ, int _index) : base(_minX, _maxX, _minZ, _maxZ, _index)
        {
            minX = _minX;
            maxX = _maxX;
            minZ = _minZ;
            maxZ = _maxZ;
            index = _index;
        }

        public BossRoom() { }

        public override void SpawnTiles(DungeonGenerator dungeonGenerator)
        {
            base.SpawnTiles(dungeonGenerator);
            dungeonGenerator.dungeon[this.GetCenter()] = TileType.Victory;
        }

    }
    public class SpikeCircleRoom : Room
    {
        public SpikeCircleRoom(int _minX, int _maxX, int _minZ, int _maxZ, int _index) : base(_minX, _maxX, _minZ, _maxZ, _index)
        {
            minX = _minX;
            maxX = _maxX;
            minZ = _minZ;
            maxZ = _maxZ;
            index = _index;
        }
        public SpikeCircleRoom() { }

        public override void SpawnTiles(DungeonGenerator dungeonGenerator)
        {
            int o = 1;
            base.SpawnTiles(dungeonGenerator);
            for (int x = minX; x <= maxX; x++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    if ((x >= minX + o && x <= maxX - o && (z == minZ + o || z == maxZ - o)) || (z >= minZ + o && z <= maxZ - o && (x == minX + o || x == maxX - o)))
                    {
                        if (x != minX + ((maxX - minX) / 2) && z != minZ + ((maxZ - minZ) / 2))
                        {
                            dungeonGenerator.dungeon[new Vector3Int(x, 0, z)] = TileType.Trap;
                        }
                    }
                }
            }
        }
    }

    public class SpikeCrossRoom : Room
    {
        public SpikeCrossRoom(int _minX, int _maxX, int _minZ, int _maxZ, int _index) : base(_minX, _maxX, _minZ, _maxZ, _index)
        {
            minX = _minX;
            maxX = _maxX;
            minZ = _minZ;
            maxZ = _maxZ;
            index = _index;
        }
        public SpikeCrossRoom() { }
        public override void SpawnTiles(DungeonGenerator dungeonGenerator)
        {
            int ox = 2;
            int oz = 1;
            base.SpawnTiles(dungeonGenerator);
            for (int x = minX; x <= maxX; x++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    if (x == minX + ((maxX - minX) / 2) || z == minZ + ((maxZ - minZ) / 2))
                    {
                        if (x >= minX + ox && x <= maxX - ox && z >= minZ + oz && z <= maxZ - oz)
                        {
                            dungeonGenerator.dungeon[new Vector3Int(x, 0, z)] = TileType.Trap;
                        }
                    }
                }
            }
        }
    }
}
