using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects_Data : MonoBehaviour
{
    public bool though;

    public bool game_object;

    public bool task;


    [Header("Game_Object")]

    public string objectName = " Object's Name";

    public Object_Enum objectType;

    
   


    [Header("Thought")]

    public string thoughtText = " Thought's Text";

    public Thought_Enum thoughtType;

    //public Transform thoughtTransform;

    public Vector2 thoughtPosition;

    


    [Header("Tasks")]


    public string taskName = " Task's Name";

    public Task_Enum taskType; 

    public float duration = 3f;

    public float waitingTime = 0f;

    public int score = 20;

    public bool isDone = false;

    public Animation animation;

    public Task waitingOnTask;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
