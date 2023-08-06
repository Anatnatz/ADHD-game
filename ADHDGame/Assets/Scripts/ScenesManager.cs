using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    bool bathFirstAppearance = true;
    bool kitchenFirstAppearance = true;
    bool bedroomFirstAppearance = true;
    [SerializeField]
    introtext introText;
    [SerializeField]
    scoreController score;
    [SerializeField]
    GameObject scoreview;

    [SerializeField]
    GameObject bottomBar;
    [SerializeField]
    GameObject phone;

    void Awake()
    {
        instance = this;
    }

    public static void SwitchToScene(string name)
    {
        SoundManager.RegisterAction(SoundManager.SoundAction.click);
        SceneManager.LoadScene(name);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Bedroom":
                StartCoroutine(loadIntroText());
                break;

            case "EndLevel":
                EndLevel();
                break;
        }
    }



    private void EndLevel()
    {
        Debug.Log("finishline");
        bottomBar.gameObject.SetActive(false);
        phone.SetActive(false);


    }


    public IEnumerator loadIntroText()

    {
        scoreview.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        introText.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }


}
