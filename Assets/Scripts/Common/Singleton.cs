/// Owner: Dongjin Kuk
/// Description: We can simply create singleton Monobehaviour script by extending this script.

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_Instance;
    public static T Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = (T)FindAnyObjectByType(typeof(T));
                if (s_Instance == null)
                {
                    GameObject newObj = new GameObject(typeof(T).Name, typeof(T));
                    s_Instance = newObj.GetComponent<T>();
                }
            }
            return s_Instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
