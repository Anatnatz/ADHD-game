using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Mouse_Controller : MonoBehaviour
{
    [SerializeField]
    LayerMask mouseLayer;
    private
    Transform selected;
    [SerializeField]
    Camera camera;
    [SerializeField]
   Transform dragging;
    private Vector3 offset;
    [SerializeField]
    Transform mouseTransform;
    
    
    
    void Start()
    {
        camera = Camera.main;   
    }

    
    void Update()
    {
        float mousePositionx = Camera.main.ScreenToWorldPoint(Input.mousePosition).x+0.1f;
        float mousePositiony = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        mouseTransform.position = new Vector2  (mousePositionx, mousePositiony);

        




         if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                dragging = hit.transform;
                offset = dragging.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            dragging = null;
        }
        
        

        if (dragging != null)
            {
                dragging.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            }
        }
        
    }

    

