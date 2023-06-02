using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thoughts_Manager : MonoBehaviour
{
    public static Thoughts_Manager ThoughtsInstance;
    
    [SerializeField]
    bool Create_Thought;
    [SerializeField]
    List<Thought> thoughtsList_;
    

    // Start is called before the first frame update
    void Start()
    {
        ThoughtsInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
       if (Create_Thought) 
       {
            Create_Thought= false;
            ActivateThought(0);
       } 
    }

    

    internal void triggerThought(Thought_Enum thoughtType)
    {
        Debug.Log("search for"+ thoughtType);
        SearchThougt(thoughtType);
    }

    private void SearchThougt(Thought_Enum thoughtType)
    {
        Debug.Log("searching");
        for (int i = 0; i < thoughtsList_.Count; i++)
        {
            if (thoughtsList_[i].thoughType == thoughtType)
            {
                Debug.Log("found it" + i);
                ActivateThought(i);
            }
        }
    }

    private void ActivateThought(int i)
    {
        Thought newThought = Instantiate(thoughtsList_[i], thoughtsList_[i].data.thoughtPosition, Quaternion.identity);
        newThought.name = thoughtsList_[i].data.thoughtText;
        
        Debug.Log(thoughtsList_[i].data.thoughtText);
    }
}
