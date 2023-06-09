
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

        setTo0numOfAppearance();
    }

    private void setTo0numOfAppearance()
    {
        for (int i = 0; i < thoughtsList_.Count; i++)
        {
            if (thoughtsList_[i] != null)
            { thoughtsList_[i].numOfAppearance = 0; }
        }
    }

    public void createThought(Thought_Enum thoughtType)
    {
        thought_Transform thoughtPrefab = thought_Transform_Prefab.GetComponent<thought_Transform>();
        
       // var positionx = UnityEngine.Random.Range(-1.0f, 1.0f);
        //var positiony = UnityEngine.Random.Range(-1.0f, 1.0f);
        //Vector2 position = new Vector2(positionx, positiony);
        //Debug.Log(position);
        //Debug.Log(thoughtPrefab.thoughtPosition);
        GameObject thoughtGameObject = Instantiate(thought_Transform_Prefab, thoughtPrefab.thoughtPosition, Quaternion.identity);
        
       // GameObject thoughtGameObject = Instantiate(thought_Transform_Prefab, position, Quaternion.identity);
        thought_Transform newThought = thoughtGameObject.GetComponent<thought_Transform>();
        newThought.thoughtType = thoughtType;

        searchForThoughtType(thoughtType);
       
        //choose text from list

        string currentText =  ChooseTextFromList(thoughtsList_[currentThoughtNum]);
        
        newThought.thoughtText = currentText;

        TMP_Text thoughtTxt = newThought.transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtTxt.SetText(newThought.thoughtText);
        newThought.changeText();
        newThought.name = newThought.thoughtText;

        newThought.transform.SetParent(thoughtsParent);
        changeThoughtStatus(thoughtType, ThoughtStatus.Appeared);
        thoughtsList_[currentThoughtNum].CheckFollowingAction();
        thought_Transforms.Add(newThought);
        thoughtsList_[currentThoughtNum].numOfAppearance++;
        Debug.Log(thoughtGameObject.transform.position);
    }

    public string ChooseTextFromList(Thought thought)
    {
       Thought currentThought = thoughtsList_[currentThoughtNum];

        if (currentThought.numOfAppearance >= 1)
        {
            if (currentThought.thoughtTexts.Count > 0)

            {

                if (currentThought.thoughtTexts.Count - 1 >= currentThought.numOfAppearance)
                {
                    if (currentThought.thoughtTexts[currentThought.numOfAppearance - 1] != null)
                    {
                        return currentThought.thoughtTexts[currentThought.numOfAppearance - 1];
                    }
                    else
                    { return currentThought.thoughtText; }
                }
                else
                {
                    return currentThought.thoughtTexts[currentThought.thoughtTexts.Count - 1];
                }

            }
            else { return currentThought.thoughtText; }
        }

        else { return currentThought.thoughtText; }

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
        searchForThoughtType(thoughtType);

        if (thoughtsList_[currentThoughtNum].thoughtStatus != ThoughtStatus.Appeared)
        {
            createThought(thoughtType);
        }
    }

    internal void changeThoughtStatus(
        Thought_Enum thoughtType,
        ThoughtStatus thoughtStatus
    )
    {
        searchForThoughtType(thoughtType);
        thoughtsList_[currentThoughtNum].thoughtStatus = thoughtStatus;
    }

    internal void updateNumOfAppearanceOnApp(Thought_Enum thoughtType)
    {
        searchForThoughtType(thoughtType);
    }
}
