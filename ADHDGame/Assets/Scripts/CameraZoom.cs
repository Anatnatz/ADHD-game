using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    //public float zoom;
    //public RoomObject zoomTarget;
    public static CameraZoom cameraZoomInstance;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        cameraZoomInstance = this;
        cam= Camera.main;
        //zoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
       if (test)
        {
            test= false;
            cam.orthographicSize = 20;
        }
    }
}
