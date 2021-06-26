using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Reset), 10);
        Timer.isPaused = true;
    }

    void Reset()
    {
        LevelManager.currentLevel = 1;
        PlayerStats.health = 1;
        SceneManager.LoadScene(0);
    }

    public static IEnumerator EndGame(float time)
    {
        yield return new WaitForSeconds(time);
        LevelManager.currentLevel = 1;
        PlayerStats.health = 1;
        SceneManager.LoadScene(0);
    }
}
