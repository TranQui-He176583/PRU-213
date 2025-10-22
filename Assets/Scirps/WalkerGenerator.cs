using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{
    public enum GridType
    {
        EMPTY,
        FLOOR,
        WALL
    }

    [System.Serializable]
    public class WalkerObject
    {
        public Vector2 dir;
        public Vector2 pos;
    }

    // === Map setup ===
    [Header("Map Settings")]
    public int MapWidth = 30;
    public int MapHeight = 30;
    public int MaxWalkers = 10;
    [Range(0f, 1f)] public float FillPercent = 0.4f;
    [Range(0f, 1f)] public float ChanceWalkerChangeDir = 0.5f;
    [Range(0f, 1f)] public float ChanceWalkerSpawn = 0.05f;
    [Range(0f, 1f)] public float ChanceWalkerDestroy = 0.05f;

    // === Tilemaps ===
    [Header("Tilemaps")]
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public Tilemap emptyTilemap;

    [Header("Tiles")]
    public Tile Floor;
    public Tile Wall;
    public Tile Empty;

    private GridType[,] gridHandler;
    private List<WalkerObject> walkers;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // Clear all tilemaps before generating
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        emptyTilemap.ClearAllTiles();

        // Initialize grid
        gridHandler = new GridType[MapWidth, MapHeight];
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                gridHandler[x, y] = GridType.EMPTY;
            }
        }

        // Create first walker
        walkers = new List<WalkerObject>();
        WalkerObject walker = new WalkerObject();
        walker.dir = RandomDirection();
        walker.pos = new Vector2(Mathf.Floor(MapWidth / 2), Mathf.Floor(MapHeight / 2));
        walkers.Add(walker);

        int iterations = 0;
        do
        {
            // Mark current walker position as floor
            foreach (WalkerObject w in walkers)
            {
                gridHandler[(int)w.pos.x, (int)w.pos.y] = GridType.FLOOR;
            }

            // Randomly destroy a walker
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < ChanceWalkerDestroy && walkers.Count > 1)
                {
                    walkers.RemoveAt(i);
                    break;
                }
            }

            // Randomly change direction
            for (int i = 0; i < walkers.Count; i++)
            {
                if (Random.value < ChanceWalkerChangeDir)
                {
                    WalkerObject w = walkers[i];
                    w.dir = RandomDirection();
                    walkers[i] = w;
                }
            }

            // Randomly spawn new walker
            int walkerCount = walkers.Count;
            for (int i = 0; i < walkerCount; i++)
            {
                if (Random.value < ChanceWalkerSpawn && walkers.Count < MaxWalkers)
                {
                    WalkerObject w = new WalkerObject();
                    w.dir = RandomDirection();
                    w.pos = walkers[i].pos;
                    walkers.Add(w);
                }
            }

            // Move walkers
            for (int i = 0; i < walkers.Count; i++)
            {
                WalkerObject w = walkers[i];
                w.pos += w.dir;
                w.pos.x = Mathf.Clamp(w.pos.x, 1, MapWidth - 2);
                w.pos.y = Mathf.Clamp(w.pos.y, 1, MapHeight - 2);
                walkers[i] = w;
            }

            // Check filled area
            float floorCount = 0;
            foreach (GridType t in gridHandler)
            {
                if (t == GridType.FLOOR) floorCount++;
            }

            if (floorCount / (MapWidth * MapHeight) > FillPercent)
            {
                break;
            }

            iterations++;
        } while (iterations < 100000);

        // Add wall borders around floors
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                if (gridHandler[x, y] == GridType.FLOOR)
                    continue;

                bool hasFloorNeighbour = false;
                for (int nx = -1; nx <= 1; nx++)
                {
                    for (int ny = -1; ny <= 1; ny++)
                    {
                        int checkX = x + nx;
                        int checkY = y + ny;
                        if (checkX < 0 || checkX >= MapWidth || checkY < 0 || checkY >= MapHeight)
                            continue;
                        if (gridHandler[checkX, checkY] == GridType.FLOOR)
                        {
                            hasFloorNeighbour = true;
                            break;
                        }
                    }
                }
                if (hasFloorNeighbour)
                    gridHandler[x, y] = GridType.WALL;
            }
        }

        // === Draw tiles ===
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                Vector3Int tilePos = new Vector3Int(x - MapWidth / 2, y - MapHeight / 2, 0);
                switch (gridHandler[x, y])
                {
                    case GridType.EMPTY:
                        emptyTilemap.SetTile(tilePos, Empty);
                        break;
                    case GridType.FLOOR:
                        floorTilemap.SetTile(tilePos, Floor);
                        break;
                    case GridType.WALL:
                        wallTilemap.SetTile(tilePos, Wall);
                        break;
                }
            }
        }

        Debug.Log("✅ Map generated with Empty, Floor, and Wall layers.");
    }

    Vector2 RandomDirection()
    {
        int choice = Mathf.FloorToInt(Random.value * 4);
        switch (choice)
        {
            case 0: return Vector2.up;
            case 1: return Vector2.down;
            case 2: return Vector2.left;
            default: return Vector2.right;
        }
    }
}
