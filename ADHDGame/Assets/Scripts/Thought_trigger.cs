using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Thought_trigger : MonoBehaviour
{
    
    private void OnTriggerEnter2D (Collider2D other)
    {


        if (other.tag == "taskApp")
        {
            //push to taslApp

            Debug.Log("push to taskApp ");
            this.gameObject.SetActive(false);
        }

        if (other.tag == "border")
        {
            //ignore task

            Debug.Log("ignore task ");
            this.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
}

