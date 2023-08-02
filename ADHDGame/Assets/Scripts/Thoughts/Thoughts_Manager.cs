
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Rendering;


public class Thoughts_Manager : MonoBehaviour
{
    public static Thoughts_Manager ThoughtsInstance;

    public List<Thought> thoughtsList_;

    [SerializeField]
    List<thought_Transform> thought_Transforms;

    [SerializeField]
    GameObject thought_Transform_Prefab;

    
    [SerializeField]
    Transform thoughtsParent;

    [SerializeField]
    thought_Transform currentThoughTransform;

    
    [SerializeField]
    int numOfNotTaskThoughtAppeared = 0;

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

         Thought currentThought = searchForThoughtType(thoughtType);

        

        Vector2 thoughtPosition;
        if (currentThought.thoughtPosition.x != 0)
        {
            thoughtPosition = currentThought.thoughtPosition;
        }
        else
        {
            thoughtPosition = new Vector2(300, 300);
        }

        string currentText = ChooseTextFromList(currentThought);

        if (GameObject.Find(currentText) == null)
        {

            SoundManager.RegisterAction(SoundManager.SoundAction.thought);
            GameObject thoughtGameObject = Instantiate(thought_Transform_Prefab, thoughtPosition, Quaternion.identity);
            thought_Transform newThought = thoughtGameObject.GetComponent<thought_Transform>();
            newThought.thoughtType = thoughtType;

            //choose text from list
            newThought.thoughtText = currentText;

            TMP_Text thoughtTxt = newThought.transform.GetChild(0).GetComponent<TMP_Text>();
            thoughtTxt.SetText(newThought.thoughtText);
            newThought.changeText();
            newThought.name = newThought.thoughtText;
            newThought.thoughtType = currentThought.thoughtType;
            newThought.taskType = currentThought.taskType;
            newThought.thoughtTransformStatus = ThoughtStatus.Appeared;
            newThought.IsItATask = currentThought.isItATask;

            newThought.transform.SetParent(thoughtsParent);
            changeThoughtStatus(thoughtType, ThoughtStatus.Appeared);
            currentThought.CheckFollowingAction();
            thought_Transforms.Add(newThought);
            currentThought.numOfAppearance++;
            

            if (currentThought.isItATask == false)
            {
                numOfNotTaskThoughtAppeared++;
                if (numOfNotTaskThoughtAppeared == 1)
                { StartCoroutine (sendInfoMessageToPlayer(newThought)); }

            }

        }
    }

    internal IEnumerator sendInfoMessageToPlayer (thought_Transform thought_Transform) 
    {
        yield return new WaitForSeconds(30);

        string currentThoughtText = thought_Transform.thoughtText;
        TMP_Text thoughtTxt = thought_Transform.transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtTxt.SetText("Swipe to the left of the screen to ignor irrelevant thougt");
        thoughtTxt.color = Color.blue;
        

        yield return new WaitForSeconds(4);
        thoughtTxt.SetText(thought_Transform.thoughtText);
        thoughtTxt.color = Color.black;
        
        yield return new WaitForSeconds(1);
        string currentThoughtText2 = thought_Transform.thoughtText;
        TMP_Text thoughtTxt2 = thought_Transform.transform.GetChild(0).GetComponent<TMP_Text>();
        thoughtTxt2.SetText("Swipe to the left of the screen to ignor irrelevant thougt");
        thoughtTxt2.color = Color.blue;

        yield return new WaitForSeconds(4);
        thoughtTxt.SetText(thought_Transform.thoughtText);
        thoughtTxt.color = Color.black;
    }


    public string ChooseTextFromList(Thought thought)
    {
        Thought currentThought = thought;

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

    

    internal void triggerThought(Thought_Enum thoughtType)
    {

        Thought currentThought = searchForThoughtType(thoughtType);

        bool isTaskDone = TaskManager.instance.IsTaskDone(currentThought.taskType);

        if (isTaskDone == false)
        {

            if (currentThought.thoughtStatus != ThoughtStatus.Appeared)
            {
                if (currentThought.showOnlyOnc == true)
                {
                    if (currentThought.numOfAppearance < 1)
                    {
                        createThought(thoughtType);
                    }
                }
                else
                {
                    if (currentThought.loop == true)
                    {
                         Debug.Log("not appeared is on loop"+ currentThought.numOfAppearance + thoughtType.ToString());
                         StartCoroutineLoop(thoughtType, currentThought);
                    }
                    else
                    {
                        createThought(thoughtType);

                    }

                }

            }
            else
            {
                if (currentThought.isOnLoop == true)
                {
                    
                    StartCoroutineLoop(thoughtType, currentThought);
                }
            }

        }
        else
        { currentThought.isOnLoop = false; }
    }

    public Thought searchForThoughtType(Thought_Enum lookForThoughtType)
    {
        Thought currentThought = null;
        for (int i = 0; i < thoughtsList_.Count; i++)
        {
            if (thoughtsList_[i].thoughtType == lookForThoughtType)
            {
                currentThought= thoughtsList_[i];
            }
        }

        return currentThought;
    }
    internal void changeThoughtStatus(Thought_Enum thoughtType, ThoughtStatus thoughtStatus)
    {
         Thought currentThought = searchForThoughtType(thoughtType);
        currentThought.thoughtStatus = thoughtStatus;
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
        Thought currentThought = searchForThoughtType(thoughtType);
       
        yield return new WaitForSeconds(currentThought.waitingGap);
        triggerThought(thoughtType);
    }

    internal void StartCoroutineLoop(Thought_Enum thoughtType, Thought thought)
    {

        StartCoroutine(StartThoughtLoop(thoughtType, thought));
    }

    internal IEnumerator StartThoughtLoop(Thought_Enum thoughtType, Thought thought)
    {
        thought.isOnLoop = true;
        createThought(thoughtType);
        yield return new WaitForSeconds(10); 
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
