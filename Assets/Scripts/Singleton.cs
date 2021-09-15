using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance != null) return instance;
            var result = FindObjectOfType<T>();
            if(result != null)
            {
                instance = result;
            }
            return instance;
        }
        set => instance = value;
    }
}