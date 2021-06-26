using UnityEngine;

public class PositionMapCamera : MonoBehaviour
{
    public void SetTransform(Vector3 position, float orthographicSize)
    {
        transform.position = position;
        GetComponent<Camera>().orthographicSize = orthographicSize;
    }
}
