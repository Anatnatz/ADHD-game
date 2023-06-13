using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class dontDestroy : MonoBehaviour
{
    public static dontDestroy instance;
    public string object_Id;

    private void Awake()
    {
         
        object_Id = name + transform.position.ToString();
    }
    void start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<dontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<dontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<dontDestroy>()[i].name == gameObject.name) 
                {
                Destroy(gameObject);
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    
}
