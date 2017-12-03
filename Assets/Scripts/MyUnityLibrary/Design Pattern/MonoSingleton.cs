using UnityEngine;

// 상속 받은 클래스에서는 생성자를 반드시 protected로 선언을 해서 외부에서의 생성을 막도록 해야한다.
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    private static bool _isAlive = true;

    public static bool IsAlive
    {
        get
        {
            if (_instance == null)
                return false;
            return _isAlive;
        }
    }

    public static T Instance
    {
        get
        {
            if (!_isAlive)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                 "' already destroyed on application quit." +
                                 " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    T[] objs = FindObjectsOfType<T>();

                    if (objs.Length > 0)
                        _instance = objs[0];

                    if (objs.Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                                       " - there should never be more than 1 singleton!" +
                                       " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        singleton.name = "(singleton) " + typeof(T);
                        _instance = singleton.AddComponent<T>();

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                                  " is needed in the scene, so '" + singleton +
                                  "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                                  _instance.gameObject.name);
                    }

                    DontDestroyOnLoad(_instance.gameObject);
                }

                return _instance;
            }
        }
    }


    private void OnDestroy()
    {
        _isAlive = false;
    }

    private void OnApplicationQuit()
    {
        _isAlive = false;
    }
}