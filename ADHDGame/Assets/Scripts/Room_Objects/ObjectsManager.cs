using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public static ObjectsManager Instance;
    public List<Room_Object> objectsScriptble;
    public List<RoomObject> objects;
    [SerializeField]
    Room_Object currentScriptbleObject;
    [SerializeField]
    RoomObject currentObject;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;  
    }

    
    public Room_Object searchInList(Object_Enum objectType)
    {
        for (int i = 0; i < objectsScriptble.Count; i++)
        {
            
            if (objectsScriptble[i].objectType == objectType)
            {
                 currentScriptbleObject = objectsScriptble[i];
            }
        }
        return currentScriptbleObject;

    }

    public RoomObject searchObject(Object_Enum objectType)
    {
        for (int i = 0; i < objects.Count; i++)
        {
          if (objects[i].objectType == objectType)
            { currentObject = objects[i]; break; }
        }
        return currentObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
