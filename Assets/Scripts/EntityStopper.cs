using UnityEngine;

public class EntityStopper : MonoBehaviour
{
    void Start()
    {
        Physics2D.gravity = Vector2.zero;
        Time.timeScale = 0;
        TileLevelGenerator.GeneratedEvent += OnGenerated;
    }

    void OnGenerated()
    {
        Physics2D.gravity = Vector2.down * 11f;
        Time.timeScale = 1;
        TileLevelGenerator.GeneratedEvent -= OnGenerated;
    }
}
