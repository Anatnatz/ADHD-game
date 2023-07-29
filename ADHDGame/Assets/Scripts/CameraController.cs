using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{

    // [SerializeField]
    // Transform border_ref;
    [SerializeField]
    float borderGap;
    public bool test;
    [SerializeField]
    Camera cam;
    public float neededZoom;
    [SerializeField]
    Vector2 zoomFocus;
    [SerializeField]
    float zoomMovement = 0.005f;
    [SerializeField]
    float currentzoom;
    [SerializeField]
    int zoomSpeed;
    public static CameraController cameraControllerInstance;


    void Start()
    {

        cameraControllerInstance = this;
        cam = Camera.main;
        currentzoom = cam.orthographicSize;
    }

    public void ZoomOnObject(Task target)
    {

        neededZoom = target.zoomNeeded;
        cam.transform.position = new Vector3(target.zoomLocation.x, target.zoomLocation.y, cam.transform.position.z);
        StartCoroutine(zooming(target));
        StartCoroutine(zoomOutTimming(target));
    }

    internal IEnumerator zooming(Task target)
    {
        while (neededZoom < currentzoom)
        {
            theZoomMovement(target);
            yield return null;
        }

    }

    private void theZoomMovement(Task target)
    {
        // StartCoroutine(zoomSteps(target));
        cam.orthographicSize = currentzoom - zoomMovement * zoomSpeed * Time.deltaTime;
        currentzoom = cam.orthographicSize;
    }

    internal IEnumerator zoomSteps(Task target)
    {
        yield return new WaitForSeconds(2);
        cam.orthographicSize = currentzoom - zoomMovement;
        currentzoom = cam.orthographicSize;
    }
    internal IEnumerator zoomOutTimming(Task target)
    {
        yield return new WaitForSeconds(2);
        cam.orthographicSize = 5.397049f;
        cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
    }

    void Update()
    {
        // Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        //  Vector2 cameraPos = Camera.main.transform.position;
        // border_ref.position = new Vector2 (cameraPos.x - borderGap, cameraPos.y);
        if (test)
        {
            test = false;
            // cam.orthographicSize = neededZoom;

        }
    }
}
