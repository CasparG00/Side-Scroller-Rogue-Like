using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelStats[] levels;

    public static int currentLevel = 1;
        
    [Serializable]
    public class LevelStats
    {
        public int maxRooms;
        
        public int golemRoomSpawnOffset = 3;
        public int batRoomSpawnOffset = 6;
        [Range(0, 1)]
        public float chestSpawnChance;
        public int maxChests = 2;
    }
}
