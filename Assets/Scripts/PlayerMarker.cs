using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
    void Start()
    {
        if (FindObjectOfType<TileLevelGenerator>() == null)
        {
            GetComponent<PlayerMarker>().enabled = false;
        }    
    }
    
    void Update()
    {
        var ratio = FindObjectOfType<TileLevelGenerator>().levelSize.x / 48 * 2;
        transform.localScale = new Vector3(1 * ratio, 2 * ratio, 1);
    }
}
