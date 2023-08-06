using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreController : MonoBehaviour
{
    public static scoreController instance;
    public TMP_Text scoreText;
    public int currentScore = 0;
    public bool scoreTest;
    

    

    private void Start()
    {
       instance= this;
        scoreText.text = "score:" + currentScore;
    }
    public void changeScore(int Score)
    {
        int newScore = currentScore + Score;
        scoreText.text = "score:" + newScore;
        currentScore = newScore;
    }

    public void Update()
    {
        if (scoreTest) 
        { 
            changeScore(2);
            scoreTest = false;
        
        }

    }

}
