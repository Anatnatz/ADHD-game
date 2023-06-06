using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

public class Thought : ScriptableObject
{
    public bool though;

    public bool game_object;

    public bool task;


    [Header("Game_Object")]

    public Object_Enum objectType;



    [Header("Thought")]

    public string thoughtText = " Thought's Text";

    public Thought_Enum thoughtType;

    public Vector2 thoughtPosition;

    public ThoughtStatus thoughtStatus;

    public bool RelevantThought;

    public int nagge;




    [Header("Tasks")]

    public Task_Enum taskType;

    

}
