using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform player;
    public Transform cam;

    bool playerTrackable = true;

    Vector3 position;
    SpriteRenderer sr;

    void Awake()
    {
        TileLevelGenerator.GeneratedEvent += OnGenerated;
    }

    void Start()
    {
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }
    
    void Update()
    {
        if (playerTrackable)
        {
            position = player.position;
        }

        position *= 8;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        position /= 8;

        transform.position = position;

        transform.GetChild(0).position = cam.position + Vector3.right;
    }

    void OnGenerated()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        print("generated");

        PlayerStats.DeathEvent += OnPlayerDeath;
        
        TileLevelGenerator.GeneratedEvent -= OnGenerated;
    }
    
    void OnPlayerDeath()
    {
        playerTrackable = false;
        sr.enabled = true;

        PlayerStats.DeathEvent -= OnPlayerDeath;
    }

    private void OnDestroy()
    {
        PlayerStats.DeathEvent -= OnPlayerDeath;
    }
}
