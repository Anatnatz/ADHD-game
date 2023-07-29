
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;


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

    [SerializeField]
    thought_Transform currentThoughTransform;

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
        SoundManager.RegisterAction(SoundManager.SoundAction.thought);

        searchForThoughtType(thoughtType);

       
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
            newThought.thoughtType = thoughtsList_[currentThoughtNum].thoughtType;
            newThought.taskType = thoughtsList_[currentThoughtNum].taskType;
            newThought.thoughtTransformStatus = ThoughtStatus.Appeared;
            newThought.IsItATask = thoughtsList_[currentThoughtNum].isItATask;

            newThought.transform.SetParent(thoughtsParent);
            changeThoughtStatus(thoughtType, ThoughtStatus.Appeared);
            thoughtsList_[currentThoughtNum].CheckFollowingAction();
            thought_Transforms.Add(newThought);
            thoughtsList_[currentThoughtNum].numOfAppearance++;

            

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
       Debug.Log("creating"+ thoughtType);
        searchForThoughtType(thoughtType);

        bool isTaskDone = TaskManager.instance.IsTaskDone(thoughtsList_[currentThoughtNum].taskType);

        if (isTaskDone == false)
        {

            if (thoughtsList_[currentThoughtNum].thoughtStatus != ThoughtStatus.Appeared)
            {
                if (thoughtsList_[currentThoughtNum].showOnlyOnc == true)
                {
                    if (thoughtsList_[currentThoughtNum].numOfAppearance < 1)
                    {
                        createThought(thoughtType);
                    }
                }
                else 
                {
                    if (thoughtsList_[currentThoughtNum].loop == true)
                    {
                        // thoughtsList_[currentThoughtNum].isOnLoop = true;
                        
                        StartCoroutineLoop(thoughtType, thoughtsList_[currentThoughtNum]);
                    }
                    else 
                    {
                        createThought(thoughtType);
                        
                    }
                     
                }

            }
            else
            {
              if(thoughtsList_[currentThoughtNum].isOnLoop == true)
                {
                    createThought(thoughtType);
                    startWaitGapThought(thoughtType);
                }
            }

        }
        else
        { thoughtsList_[currentThoughtNum].isOnLoop = false; }
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


    public void startWaitGapThought(Thought_Enum thoughtType)
    {
        StartCoroutine(waitGapThought(thoughtType));
    }

    internal IEnumerator waitGapThought(Thought_Enum thoughtType)
    {
        searchForThoughtType(thoughtType);
        Thought currentThought = thoughtsList_[currentThoughtNum];

        yield return new WaitForSeconds(currentThought.waitingGap);
        triggerThought(thoughtType);
    }

    internal void StartCoroutineLoop(Thought_Enum thoughtType, Thought thought)
    {
        
        StartCoroutine(StartThoughtLoop(thoughtType, thought));
    }

    internal IEnumerator StartThoughtLoop(Thought_Enum thoughtType, Thought thought)
    
       
        {
                yield return new WaitForSeconds(thought.loopInterval);
                thought.isOnLoop = true;
                createThought(thoughtType);
                triggerThought(thoughtType);
                          
        }
       
    

    internal thought_Transform searchForThoughtTransformTypeByTask(Task_Enum taskType)
    {

        for (int i = 0; i < thought_Transforms.Count; i++)
        {
            if (thought_Transforms[i].taskType == taskType)
            {
                currentThoughTransform = thought_Transforms[i];
            }
        }
        return currentThoughTransform;
    }

    internal thought_Transform searchForTransformByThoughtType(Thought_Enum thoughtType) 
    {
        for (int i = 0; i < thought_Transforms.Count; i++)
        {
            if (thought_Transforms[i].thoughtType == thoughtType)
            {
                currentThoughTransform = thought_Transforms[i];
            }
        }
        return currentThoughTransform;
    }

}
