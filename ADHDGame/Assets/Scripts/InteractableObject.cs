using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskList", menuName = "ScriptableObjects/List")]
public class InteractableObject : ScriptableObject
{
    [Serializable]
    public class RelatedTask
    {
        public Task task;

        public RelatedTask(Task _task)
        {
            task = _task;
        }
    }
}
