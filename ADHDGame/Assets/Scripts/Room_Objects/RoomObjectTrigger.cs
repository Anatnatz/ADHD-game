using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObjectTrigger : MonoBehaviour
{
    [SerializeField]
    RoomObject roomObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags_Enum.Mouse.ToString())
        {
            roomObject.objectTrigger();
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
