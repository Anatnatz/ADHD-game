using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextOnApp : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    AppTransform appTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (appTransform.setText)
        {
            text.text = appTransform.appTransformText;
        }
    }
}
