using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thoughts_Manager : MonoBehaviour
{
    public static Thoughts_Manager ThoughtsInstance;

   public List<Thought> thoughtsList_;
    [SerializeField]
    List<thought_Transform> thought_Transforms;
    [SerializeField]
    thought_Transform thought_Transform_Prefab;
    public int currentThoughtNum;


    // Start is called before the first frame update
    void Start()
    {
        ThoughtsInstance = this;
    }

    void createThought(Thought_Enum thoughtType)
    {
        thought_Transform newThought = Instantiate(thought_Transform_Prefab, thought_Transform_Prefab.thoughtPosition, Quaternion.identity);
        newThought.thoughtType = thoughtType;
        
        searchForThoughtType(thoughtType);

        newThought.thoughtText = thoughtsList_[currentThoughtNum].thoughtText;
        newThought.changeText();
        newThought.name = thoughtsList_[currentThoughtNum].thoughtText ;
        
        changeThoughtStatus(thoughtType, ThoughtStatus.Appeared);
        thought_Transforms.Add(newThought);
    }

  

    public void searchForThoughtType(Thought_Enum lookForThoughtType)
    {
        
        for (int i = 0; i < thoughtsList_.Count; i++)
        {
            if (thoughtsList_[i].thoughtType == lookForThoughtType)
            {
                currentThoughtNum = i;
                
            }


        }
    }

    private void updateThoughtStatus(int currentThoughtNum)
    {
        thoughtsList_[currentThoughtNum].thoughtStatus = ThoughtStatus.Appeared;
    }


    internal void triggerThought(Thought_Enum thoughtType)
    {
        Debug.Log("create thought" + thoughtType);
        createThought(thoughtType);
    }

    internal void changeThoughtStatus(Thought_Enum thoughtType, ThoughtStatus thoughtStatus)
    {
        searchForThoughtType(thoughtType);
        thoughtsList_[currentThoughtNum].thoughtStatus = thoughtStatus;
    }

    internal void updateNumOfAppearanceOnApp(Thought_Enum thoughtType)
    {
        searchForThoughtType(thoughtType);
        
    }
}

