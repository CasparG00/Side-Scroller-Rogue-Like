using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
    public float cameraSpeed;
    public Transform objectToFollow;

    Vector3 targetPosition;
    bool playerIsDead;

    void Awake()
    {
        TileLevelGenerator.GeneratedEvent += OnLevelGenerated;
        PlayerStats.DeathEvent += OnPlayerDeath;
    }

    void LateUpdate()
    {
        var currentPosition = transform.position;

        transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * cameraSpeed);
        
        if (!playerIsDead)
        {
            targetPosition.x = objectToFollow.position.x;
            if (objectToFollow.transform.GetComponent<Movement>().isGrounded)
            {
                targetPosition.y = objectToFollow.position.y;
            }
        }
    }

    void OnPlayerDeath()
    {
        playerIsDead = true;
        
        targetPosition.x -= 2;
        targetPosition.y -= 1;

        PlayerStats.DeathEvent -= OnPlayerDeath;
    }

    void OnLevelGenerated()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        TileLevelGenerator.GeneratedEvent -= OnLevelGenerated;
    }
}