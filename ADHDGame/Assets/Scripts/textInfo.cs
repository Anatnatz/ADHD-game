using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class textInfo : MonoBehaviour
{
    
    public TMP_Text text;
    [SerializeField]
    Button x;
   [SerializeField]
    bool on;
    [SerializeField]
    InfoManager infoManager;
    
    

    private void start()
    {
        

    }

    
    
    public void NewInfo (string info)
    {
      text.text = info;
        setActiveXButtonON(); 


    }

    public void setActiveXButtonON()
    { x.gameObject.SetActive(true); }

    public void setActiveXButtonOFF()
    { x.gameObject.SetActive(false); }

    public void CloseInfo ()
    {
        Debug.Log("ok");
        NewInfo("");
        infoManager.ClosePanel();
    }

    public void Update()
    {
        if(on) 
        {
            NewInfo(" HI");
            on= false;
           
        }
        
    }
}
