using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOnHover : MonoBehaviour
{
    [SerializeField]
    public string hoveredText;
    [SerializeField]
    GameObject textObject;
  
    void Start()
    {
        SetObjectText();
    }

    void SetObjectText()
    {
        TextMesh objTxtComponent = transform.GetComponentInChildren<TextMesh>();
        textObject = objTxtComponent.gameObject;
        Debug.Log(objTxtComponent);
        objTxtComponent.text = hoveredText;
        textObject.SetActive(false);
        
    }

    void OnMouseEnter()
    {
        textObject.SetActive(true);
    }

    void OnMouseExit()
    {
        textObject.SetActive(false);
    }
}
