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
    }

    private static T m_instance = null;
}