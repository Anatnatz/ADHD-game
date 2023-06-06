using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTaskApp : MonoBehaviour

{
    [SerializeField]
    AppTransform appTransform;
    [SerializeField]
    Task_Enum taskType;
    [SerializeField]
    string taskName;


    // Start is called before the first frame update
    void Start()
    {
        taskType = appTransform.taskType;
        taskName = appTransform.name;

        // Update is called once per frame
        void Update()
        {

        }
    }
}