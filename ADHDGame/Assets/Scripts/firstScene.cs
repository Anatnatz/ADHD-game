using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstScene : MonoBehaviour
{
    [SerializeField]
    GameObject logo;

    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(startLogo());  
    }

    internal IEnumerator startLogo() 
    {
        yield return new WaitForSeconds(3);
        logo.SetActive(true);
    }
}
