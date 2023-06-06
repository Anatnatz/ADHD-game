using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtText : MonoBehaviour
{
   public TMP_Text text;
   public thought_Transform thoughtTransform;

    // Start is called before the first frame update
    
    
    void Update()
    {
        if (thoughtTransform.setText)
        {
            text.text = thoughtTransform.thoughtText;
        }
    }

   
}
