using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dont : MonoBehaviour
{
    public static dont instance;

    void awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
}
