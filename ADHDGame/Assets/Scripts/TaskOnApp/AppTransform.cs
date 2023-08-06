using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppTransform : MonoBehaviour
{
    public Task_Enum taskType;

    
    public Vector2 appTransformPosition;
    
    public string appTransformText;
    
    public bool setText;
    
    public TextOnApp_Enum taskOnAppStatus;

    public int positionOnApp;

    public bool appeared;

    public TMP_Text v;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void changeText()
    {
        setText= true;
    }

   

}
