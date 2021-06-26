using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    private LevelManager lm;
    
    void Start()
    {
        lm = FindObjectOfType<LevelManager>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {

        if (LevelManager.currentLevel < lm.levels.Length)
        {
            LevelManager.currentLevel++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
