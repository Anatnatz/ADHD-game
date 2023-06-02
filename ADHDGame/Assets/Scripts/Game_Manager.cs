using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager gameInstance;

    // Start is called before the first frame update
    void Start()
    {
        gameInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
