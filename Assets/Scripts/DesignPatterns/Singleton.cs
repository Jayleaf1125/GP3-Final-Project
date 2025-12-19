using UnityEngine;

// Credits: Samson

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance {  get; private set; }

    /// <summary>
    ///     Make sure this is called on override when inheriting this class
    /// </summary>
    /// 
    private protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
