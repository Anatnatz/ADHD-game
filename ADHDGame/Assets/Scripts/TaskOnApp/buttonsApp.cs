using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class buttonsApp : MonoBehaviour
{
    [SerializeField]
    AppTransform appTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkAsDone()
    {
        TaskOnApp_Manager.TaskOnAppInstance.changeStatus(appTransform.gameObject.name, TextOnApp_Enum.Marked_As_Done);
    }

    public void deleteFromApp()
    {
        TaskOnApp_Manager.TaskOnAppInstance.changeStatus(appTransform.gameObject.name, TextOnApp_Enum.Deleted);
    }
}
