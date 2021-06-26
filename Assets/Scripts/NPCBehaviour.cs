using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    void Update()
    {
        GetComponent<SpriteRenderer>().flipX = transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x;;
    }
}
