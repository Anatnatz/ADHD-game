using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public Image infoPanel;
    public textInfo infoTextCode;
    [SerializeField]
    bool test;

    // Start is called before the first frame update
    void Start()
    {
     infoPanel.gameObject.SetActive (false);   
    }

    public void OpenPanel()
    { infoPanel.gameObject.SetActive (true);}

    public void ClosePanel()
    { infoPanel.gameObject.SetActive (false);}

    // Update is called once per frame
    void Update()
    {
        if (test) 
        {
        test = false;
            OpenPanel();
            infoTextCode.NewInfo("HI");
        }
        
    }
}
