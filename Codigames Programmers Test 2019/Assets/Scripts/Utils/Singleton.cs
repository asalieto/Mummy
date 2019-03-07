using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : class, new()
{
    public static T Instance 
    {
        get 
        {
            return m_instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (m_instance == null)
        {
            m_instance = this as T;
        }
        else
        {
            Debug.Log(name + ": Error: already initialized");
        }
    }

    private static T m_instance = null;
}