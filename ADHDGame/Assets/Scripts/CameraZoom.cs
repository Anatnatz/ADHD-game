using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector3 camOrigin;
    public Camera cam;
    public float speed;
    public static CameraZoom instance;

    float minX;
    float minY;
    float maxX;
    float maxY;
    float vertExtent;
    float horzExtent;
    void Start()
    {
        instance = this;
        cam = Camera.main;
        vertExtent = cam.orthographicSize * 2f;

        horzExtent = vertExtent * cam.aspect;
    }

    void RecalculateBounds()
    {

        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        // Calculations assume map is position at the origin
        minX = ((+width - horzExtent) / 2.0f);
        maxX = ((-width + horzExtent) / 2.0f);
        minY = ((+height - vertExtent) / 2.0f);
        maxY = ((-height + vertExtent) / 2.0f);
    }

    public IEnumerator ZoomIn(Vector3 target, float zoomAmount = 3)
    {
        while (cam.orthographicSize > zoomAmount)
        {
            target.z = -10;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomAmount - 0.5f, speed);
            RecalculateBounds();
            Vector3 targetBounds = new Vector3(Mathf.Clamp(target.x, minX, maxX), Mathf.Clamp(target.y, minY, maxY), -10);
            Vector3 camBounds = new Vector3(Mathf.Clamp(cam.transform.position.x, minX, maxX), Mathf.Clamp(cam.transform.position.y, minY, maxY), -10);
            cam.transform.position = Vector3.Lerp(camBounds, target, speed);
            // cam.transform.position = camBounds;
            yield return null;
        }
    }
    public IEnumerator ZoomOut()
    {
        float originZoom = 5.397049f;
        while (cam.orthographicSize < originZoom)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5.5f, speed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, camOrigin, speed);
            yield return null;

        }
    }
}
