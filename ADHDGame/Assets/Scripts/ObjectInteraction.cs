using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    Objects_Data object_data;

    void Start()
    {
        object_data = GetComponent<Objects_Data>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
