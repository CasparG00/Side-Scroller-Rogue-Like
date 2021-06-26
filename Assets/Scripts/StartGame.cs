using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Timer.currentTime = Timer.MAXTime;
        Timer.isPaused = false;
    }
}
