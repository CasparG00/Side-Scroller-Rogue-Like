using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject mapRoomTile;
    
    public void AddRoom(Vector3 position, Vector2Int size)
    {
        var instance = Instantiate(mapRoomTile, position, Quaternion.identity);
        instance.transform.localScale = new Vector3(size.x, size.y, 1);
    }
}
