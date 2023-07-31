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
    public bool isZoom;
    //public Vector2[] newTarget;
    public Vector2 camPoisition;


    void Start()
    {

        cameraControllerInstance = this;
        cam = Camera.main;
        camPoisition = cam.transform.position;
        //currentzoom = cam.orthographicSize;
    }

    public void ZoomOnObject(Task target)
    {

        neededZoom = target.zoomNeeded;
        cam.transform.position = new Vector3(target.zoomLocation.x, target.zoomLocation.y, cam.transform.position.z);
        StartCoroutine(zooming(target));
        StartCoroutine(zoomOutTimming(target));
    }

    public void zoom(Task target)
    {
        isZoom = true;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, zoomSpeed);
        Vector2 zoomLocation = new Vector2(target.zoomLocation.x, target.zoomLocation.y);
        Debug.Log(zoomLocation);
        cam.transform.position = Vector2.Lerp(cam.transform.position, zoomLocation, zoomSpeed);
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
        isZoom = false;
        yield return new WaitForSeconds(2);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5.397049f, zoomSpeed);
        //cam.transform.position = Vector2.Lerp(cam.transform.position, camPoisition, zoomSpeed);
        //cam.orthographicSize = 5.397049f;
        // cam.transform.position = new Vector3(0, 0, cam.transform.position.z);
    }

    void Update()
    {
        if (isZoom)
        {
            Task target = TaskManager.instance.searchTaskOnList(Task_Enum.Drink_water);
            zoom(target);
        }
    }
}
