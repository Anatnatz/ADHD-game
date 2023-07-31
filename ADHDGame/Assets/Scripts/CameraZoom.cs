using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector3 camOrigin;
    public Camera cam;
    public float speed;
    public bool isZooming;
    public static CameraZoom instance;
    void Start()
    {
        instance = this;
        cam = Camera.main;
        isZooming = true;
    }

    public IEnumerator ZoomIn(Vector3 target, float zoomAmount = 3)
    {
        while (cam.orthographicSize > zoomAmount)
        {
            Debug.Log(zoomAmount + " sss " + cam.orthographicSize);
            target.z = -10;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomAmount - 0.5f, speed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, target, speed);
            yield return null;
        }
    }
    public IEnumerator ZoomOut()
    {
        float originZoom = 5.397049f;
        while (cam.orthographicSize < originZoom)
        {
            Debug.Log(originZoom + " sss " + cam.orthographicSize);

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5.5f, speed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, camOrigin, speed);
            yield return null;

        }
    }
}
