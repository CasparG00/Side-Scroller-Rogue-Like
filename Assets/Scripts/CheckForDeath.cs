using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckForDeath : MonoBehaviour
{
    void Start()
    {
        PlayerStats.DeathEvent += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        StartCoroutine(Reset());
        PlayerStats.DeathEvent -= OnPlayerDeath;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(5);
        
        LevelManager.currentLevel = 1;
        PlayerStats.health = 1;
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        PlayerStats.DeathEvent -= OnPlayerDeath;
    }
}
