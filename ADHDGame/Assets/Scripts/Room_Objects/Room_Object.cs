using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]


public class Room_Object : ScriptableObject
{
   


    [Header("Game_Object")]

    public string objectName = " Object's Name";

    public Object_Enum objectType;

    public float timeToAppeare;





    [Header("Thought")]

    public Thought_Enum thoughtType;
    public Vector2 thoughtPosition;




    [Header("Tasks")]

    public Task_Enum taskType;
}
