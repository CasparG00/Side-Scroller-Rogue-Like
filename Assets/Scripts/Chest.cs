using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite open;
    public GameObject[] items;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        sr.sprite = open;

        var item = items[Random.Range(0, items.Length)];
        Instantiate(item, transform.position + Vector3.up, Quaternion.identity);

        Destroy(GetComponent<BoxCollider2D>());
    }
}
