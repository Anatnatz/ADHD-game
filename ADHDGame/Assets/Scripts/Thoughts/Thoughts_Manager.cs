using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Thoughts_Manager : MonoBehaviour
{
    public static Thoughts_Manager ThoughtsInstance;

    public List<Thought> thoughtsList_;

    [SerializeField]
    List<thought_Transform> thought_Transforms;

    [SerializeField]
    GameObject thought_Transform_Prefab;

    public int currentThoughtNum;

    [SerializeField]
    Transform thoughtsParent;

    // Start is called before the first frame update
    void Start()
    {
        ThoughtsInstance = this;
        for (int i = 0; i < thoughtsList_.Count; i++)
        {
            thoughtsList_[i].thoughtStatus = ThoughtStatus.None;
        }
    }

    public void createThought(Thought_Enum thoughtType)
    {
        thought_Transform thoughtPrefab =  thought_Transform_Prefab.GetComponent<thought_Transform>();
        GameObject thoughtGameObject =  Instantiate(thought_Transform_Prefab,thoughtPrefab.thoughtPosition, Quaternion.identity);
        thought_Transform newThought = thoughtGameObject.GetComponent<thought_Transform>();
        newThought.thoughtType = thoughtType;

        searchForThoughtType (thoughtType);

        newThought.thoughtText = thoughtsList_[currentThoughtNum].thoughtText;
        TMP_Text thoughtTxt =
            newThought.transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtTxt.SetText(newThought.thoughtText);
        newThought.changeText();
        newThought.name = thoughtsList_[currentThoughtNum].thoughtText;

        newThought.transform.SetParent (thoughtsParent);
        changeThoughtStatus(thoughtType, ThoughtStatus.Appeared);
        thought_Transforms.Add (newThought);
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

    internal void triggerThought(Thought_Enum thoughtType)
    {
        searchForThoughtType (thoughtType);

        if (thoughtsList_[currentThoughtNum].thoughtStatus == ThoughtStatus.None
        )
        {
            createThought (thoughtType);
        }
    }

    internal void changeThoughtStatus(
        Thought_Enum thoughtType,
        ThoughtStatus thoughtStatus
    )
    {
        searchForThoughtType (thoughtType);
        thoughtsList_[currentThoughtNum].thoughtStatus = thoughtStatus;
    }

    internal void updateNumOfAppearanceOnApp(Thought_Enum thoughtType)
    {
        searchForThoughtType (thoughtType);
    }
}
