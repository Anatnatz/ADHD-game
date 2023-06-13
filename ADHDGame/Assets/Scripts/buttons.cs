using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttons : MonoBehaviour
{
   
    [SerializeField]
    string scene;
    public void LoadScene (string sceneName)
    {
        SceneManager.LoadScene ("Kitchen");
    }

    
}
