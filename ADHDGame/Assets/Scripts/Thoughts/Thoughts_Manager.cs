
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
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

       
        searchForThoughtType(thoughtType);
        if (thoughtsList_[currentThoughtNum].loop == true) 
        {
            thoughtsList_[currentThoughtNum].isOnLoop = true;
           
        }
        Vector2 thoughtPosition;
        if (thoughtsList_[currentThoughtNum].thoughtPosition.x != 0)
        {
            thoughtPosition = thoughtsList_[currentThoughtNum].thoughtPosition;
        }
        else
        {
            thoughtPosition = new Vector2(300, 300);
        }

        string currentText = ChooseTextFromList(thoughtsList_[currentThoughtNum]);

        if (GameObject.Find(currentText) == null)
        {

            GameObject thoughtGameObject = Instantiate(thought_Transform_Prefab, thoughtPosition, Quaternion.identity);
            thought_Transform newThought = thoughtGameObject.GetComponent<thought_Transform>();
            newThought.thoughtType = thoughtType;

            //choose text from list
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

        bool isTaskDone = TaskManager.instance.IsTaskDone(thoughtsList_[currentThoughtNum].taskType);

        if (isTaskDone == false)
        {

            if (thoughtsList_[currentThoughtNum].thoughtStatus != ThoughtStatus.Appeared)
            {
                createThought(thoughtType);
            }

        }
    }

    internal void changeThoughtStatus( Thought_Enum thoughtType, ThoughtStatus thoughtStatus )
    {
        searchForThoughtType(thoughtType);
        thoughtsList_[currentThoughtNum].thoughtStatus = thoughtStatus;
    }

    internal void updateNumOfAppearanceOnApp(Thought_Enum thoughtType)
    {
        searchForThoughtType(thoughtType);
    }

    
    public void startWaitGapThought(Thought_Enum thoughtType)
    {
        StartCoroutine(waitGapThought(thoughtType));
    }

    internal IEnumerator waitGapThought(Thought_Enum thoughtType)
    {
        Thoughts_Manager.ThoughtsInstance.searchForThoughtType(thoughtType);
        Thought currentThought = Thoughts_Manager.ThoughtsInstance.thoughtsList_[Thoughts_Manager.ThoughtsInstance.currentThoughtNum];

        yield return new WaitForSeconds(currentThought.waitingGap);
        Thoughts_Manager.ThoughtsInstance.createThought(thoughtType);
    }

    internal void StartCoroutineLoop(Thought_Enum thoughtType, Thought thought)
    {
        StartCoroutine(StartThoughtLoop(thoughtType, thought));
    }

    internal IEnumerator StartThoughtLoop(Thought_Enum thoughtType, Thought thought)
    {
        yield return new WaitForSeconds(thought.loopInterval);
        createThought(thoughtType);
    }
}
