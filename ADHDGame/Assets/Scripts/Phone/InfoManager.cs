using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance;
   public Image infoPanel;
    public textInfo infoTextCode;
    [SerializeField]
    bool test;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        infoPanel.gameObject.SetActive(false);
    }

    public void OpenPanel()
    { infoPanel.gameObject.SetActive(true); }

   public void ClosePanel()
    { infoPanel.gameObject.SetActive(false); }

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

    public void SendInfoMessage(string info)
    {
        OpenPanel();
        infoTextCode.NewInfo(info);
        StartCoroutine(CloseMessage());
    }

    IEnumerator CloseMessage()
    {
        yield return new WaitForSeconds(6f);
       ClosePanel();
    }
}
