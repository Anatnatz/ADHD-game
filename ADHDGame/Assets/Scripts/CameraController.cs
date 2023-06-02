using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    int zoomSpeed;
    [SerializeField]
    Transform border_ref;
    [SerializeField]
    float borderGap;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Vector2 cameraPos = Camera.main.transform.position;
        border_ref.position = new Vector2 (cameraPos.x - borderGap, cameraPos.y);
    }
}
