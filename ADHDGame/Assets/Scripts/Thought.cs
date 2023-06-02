using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : MonoBehaviour
{
    public Thought_Enum thoughType;
    public Objects_Data data;

    private void Start()
    {
        this.name = data.thoughtText;
        
    }
}
