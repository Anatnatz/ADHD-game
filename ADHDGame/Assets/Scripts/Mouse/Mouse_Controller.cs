using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Mouse_Controller : MonoBehaviour
{
    [SerializeField]
    LayerMask mouseLayer;

    private Transform selected;

    [SerializeField]
    Camera camera;

    [SerializeField]
    Transform dragging;

    private Vector3 offset;

    [SerializeField]
    Transform mouseTransform;

    [SerializeField]
    MessageController messageController;
    [SerializeField]
    public TMP_Text transformText;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        float mousePositionx = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + 0.1f;
        float mousePositiony = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        mouseTransform.position = new Vector2(mousePositionx, mousePositiony);

        //  float transformTextPositionX = mouseTransform.position.x + 1f;
        //float transformTextPositionY = mouseTransform.position.y;
        //transformText.transform.position = new Vector2(transformTextPositionX, transformTextPositionY);

        //GetMouseButtonDown:
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit =
                Physics2D
                    .Raycast(Camera
                        .main
                        .ScreenToWorldPoint(Input.mousePosition),
                    Vector2.zero);
            if (hit)
            {
                //Thought:
                if (hit.transform.tag == Tags_Enum.Thought.ToString())
                {
                    dragging = hit.transform;
                    offset =
                        dragging.position -
                        Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

                //DeleteTaskApp:
                if (hit.transform.tag == Tags_Enum.DeleteTaskApp.ToString())
                {
                    Debug.Log(hit.transform.name);

                    TaskOnApp_Manager
                        .TaskOnAppInstance.changeStatus(hit.transform.name, TextOnApp_Enum.Marked_As_Done);

                }


            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = null;
        }

        if (dragging != null)
        {
            dragging.position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }
}
