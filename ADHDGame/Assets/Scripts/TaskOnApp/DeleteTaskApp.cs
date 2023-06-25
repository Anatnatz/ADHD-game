using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTaskApp : MonoBehaviour
{
    [SerializeField]
    AppTransform appTransform;

    [SerializeField]
    Task_Enum taskType;

    // Start is called before the first frame update
    void Start()
    {
        taskType = appTransform.taskType;
        this.name = appTransform.gameObject.name;
    }
}
