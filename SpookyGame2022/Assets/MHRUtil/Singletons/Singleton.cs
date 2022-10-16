using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T Instance;

    protected virtual void Awake()
    {
        Instance = GetComponent<T>();
    }
}