using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Application.Quit();
    }
}
