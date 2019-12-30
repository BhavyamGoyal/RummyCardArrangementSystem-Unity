using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }

    public virtual void Awake()
    {
        instance = FindObjectOfType<T>();
        DontDestroyOnLoad(instance);
    }
}