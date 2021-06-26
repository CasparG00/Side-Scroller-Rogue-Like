using UnityEngine;

public class ProgressUpdater : MonoBehaviour
{
    public LevelManager lm;
    
    public GameObject progressItem;

    public Sprite level;
    public Sprite levelCleared;

    void Start()
    {
        for (var i = 0; i < lm.levels.Length; i++)
        {
            Instantiate(progressItem, transform.position + Vector3.right * i, Quaternion.identity, transform);
        }
        
        for (var i = 0; i < transform.childCount; i++)
        {
            var sr = transform.GetChild(i).GetComponent<SpriteRenderer>();
            sr.sprite = i < LevelManager.currentLevel ? levelCleared : level;
        }
    }
}
