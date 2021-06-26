using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileLevelGenerator : MonoBehaviour
{
    public Tilemap tm;
    public TileBase baseTile;

    [Space]
    public Vector2Int minRoomSize;
    public Vector2Int maxRoomSize;

    public int maxRoomDescend = 3;

    [Space]
    public GameObject spikes;
    public int maxSpikes = 5;
    
    [Space]
    public GameObject golem;
    public GameObject bat;
    public GameObject chest;

    [Space]
    public Tile ladder;
    public GameObject levelExitZone;
    public GameObject player;

    [Space]
    public GenerateMap gm;
    public float mapZOffset = 10;
    
    LevelManager lm;
    LevelManager.LevelStats levelStats;
    
    public static event Action GeneratedEvent;

    public Vector2 levelSize;

    void Start()
    {
        lm = GetComponent<LevelManager>();
        levelStats = lm.levels[LevelManager.currentLevel - 1];

        CreateBaseTiles();
        CreateRooms();

        var pos = new Vector3(levelSize.x * 0.5f, levelSize.y * 0.5f, -10);

        var camWidth = levelSize.x * 0.5f;
        var screenAspect = Screen.width / Screen.height;
        var orthoSize = camWidth / screenAspect * 0.8f;

        FindObjectOfType<PositionMapCamera>().SetTransform(pos, orthoSize);
        
        GeneratedEvent?.Invoke();
    }

    void CreateBaseTiles() 
    {
        var fromX = -10;
        var fromY = (maxRoomSize.y + maxRoomDescend * levelStats.maxRooms + 10) * -1;

        var toX = maxRoomSize.x * levelStats.maxRooms + 10;
        var toY = 20;

        var from = new Vector2Int(fromX, fromY);
        var to = new Vector2Int(toX, toY);

        for (var y = from.y; y < to.y; y++)
        {
            for (var x = from.x; x < to.x; x++)
            {
                var position = new Vector3Int(x, y, 0);
                tm.SetTile(position, baseTile);
            }
        }
    }

    void CreateRooms() 
    {
        var tilePos = tm.WorldToCell(Vector3.zero);
        var lastPosition = tilePos;

        var chestCount = 0;

        for (var i = 0; i < levelStats.maxRooms; i++)
        {
            var roomSize = RandomVector3Int(minRoomSize, maxRoomSize);
            
            levelSize.x += roomSize.x;
            
            ClearTilesInArea(lastPosition, lastPosition + roomSize);
            var pos = new Vector3(roomSize.x * 0.5f, roomSize.y * 0.5f, mapZOffset);
            gm.AddRoom(lastPosition + pos, (Vector2Int)roomSize);

            var spikePosition = 0;
            var spikeWidth = 0;

            if (i > 0 && i < levelStats.maxRooms - 1) 
            {
                //Spawn Spikes Randomly
                if (roomSize.x > 3)
                {
                    spikePosition = Random.Range(0, roomSize.x - 1);
                    var max = Mathf.Min(roomSize.x - spikePosition, maxSpikes);
                    spikeWidth = Random.Range(1, max);

                    var origin = lastPosition + Vector3Int.right * spikePosition;

                    ClearTilesInArea(origin + Vector3Int.down * 2, origin + Vector3Int.right * spikeWidth);

                    for (var x = 0; x < spikeWidth; x++)
                    {
                        Instantiate(spikes, origin + Vector3Int.down * 2 + Vector3Int.right * x, Quaternion.identity);
                    }
                }
            }
            
            if (i >= levelStats.maxRooms - 1)
            {
                var roomCenter = Mathf.RoundToInt(roomSize.x * 0.5f);
                ClearTilesInArea(lastPosition + new Vector3Int(roomCenter - 1, -10, 0),
                    lastPosition + new Vector3Int(roomCenter + 2, 0, 0));

                
                for (var y = 0; y < 10; y ++)
                {
                    var position = lastPosition + Vector3Int.right * roomCenter;
                    tm.SetTile(position + Vector3Int.down * y, ladder);
                }

                var exitPosition = lastPosition + Vector3Int.right * roomCenter + Vector3Int.down * 8;
                Instantiate(levelExitZone, exitPosition, Quaternion.identity);
            }
            else if (i == 0)
            {
                var roomCenter = Mathf.RoundToInt(roomSize.x * 0.5f);
                ClearTilesInArea(lastPosition + Vector3Int.right * (roomCenter - 1),
                    lastPosition + Vector3Int.right * (roomCenter + 2) + Vector3Int.up * (10 + roomSize.y));
                
                for (var y = 0; y < 10; y ++)
                {
                    var position = lastPosition + Vector3Int.right * roomCenter;
                    tm.SetTile(position + Vector3Int.up * y, ladder);
                }

                var playerPosition = lastPosition + Vector3Int.right * roomCenter + Vector3Int.up * 10;
                Instantiate(player, playerPosition, Quaternion.identity);
            }

            //Spawn golem randomly
            if (i > levelStats.golemRoomSpawnOffset && roomSize.x > 4 && spikeWidth < roomSize.x * 0.5f)
            {
                Vector3 offset;
                if (spikePosition > roomSize.x - (spikePosition + spikeWidth))
                {
                    offset = Vector3.right * Random.Range(0, spikePosition);
                }
                else
                {
                    offset = Vector3.right * Random.Range(spikePosition + spikeWidth, roomSize.x);
                }

                Instantiate(golem, lastPosition + offset + Vector3.up, Quaternion.identity);
            }

            //Spawn bat randomly
            if (i > levelStats.batRoomSpawnOffset)
            {
                var spawnOffset = Vector3.right * Random.Range(0, roomSize.x) + Vector3.up * Random.Range(0, roomSize.y - 1);
                Instantiate(bat, lastPosition + spawnOffset, Quaternion.identity);
            }

            if (chestCount < levelStats.maxChests) 
            {
                if (Random.value < levelStats.chestSpawnChance && spikeWidth < roomSize.x * 0.5f)
                {
                    var offset = Vector3.right * Random.Range(spikePosition + spikeWidth, roomSize.x);

                    Instantiate(chest, lastPosition + offset, Quaternion.identity);
                    chestCount++;
                }
            }

            lastPosition.x += roomSize.x;
            lastPosition.y -= Random.Range(0, maxRoomDescend);
        }
    }

    void ClearTilesInArea(Vector3Int from, Vector3Int to)
    {
        for (var y = 0; y < to.y - from.y; y++)
        {
            for (var x = 0; x < to.x- from.x; x++)
            {
                var position = new Vector3Int(from.x + x, from.y + y, 0);
                tm.SetTile(position, null);
            }
        }
    }

    Vector3Int RandomVector3Int(Vector2Int min, Vector2Int max)
    {
        return new Vector3Int(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);
    }
}
