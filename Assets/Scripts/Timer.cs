using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public const int MAXTime = 180;

    public static float currentTime;

    TextMeshProUGUI timer;
    public TextMeshProUGUI shadow;

    public static bool isPaused;

    private Transform player;

    void Awake()
    {
        timer = GetComponent<TextMeshProUGUI>();
        PlayerStats.DeathEvent += OnPlayerDeath;
        TileLevelGenerator.GeneratedEvent += OnGenerated;
    }

    void Update()
    {
        if (currentTime > 0 && !isPaused)
        {
            currentTime -= Time.deltaTime;
        }

        if (currentTime <= 0 && player != null)
        {
            player.GetComponent<IDamageable>().TakeDamage(2);
        }
        
        timer.text = Mathf.Ceil(currentTime).ToString();
        shadow.text = timer.text;
    }

    void OnGenerated()
    {
        player = Player.instance.transform;
        TileLevelGenerator.GeneratedEvent -= OnGenerated;
    }

    void OnPlayerDeath()
    {
        isPaused = true;
        PlayerStats.DeathEvent -= OnPlayerDeath;
    }
}
