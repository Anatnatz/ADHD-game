using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreController : MonoBehaviour
{
    public static scoreController instance;
    public TMP_Text scoreText;
    

    private void Start()
    {
        scoreText.text = "score:" + 0;
    }
    public void changeScore(int curruntScore)
    {
        scoreText.text = "score:" + curruntScore;
    }

    
}
