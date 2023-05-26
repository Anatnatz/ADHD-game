using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskList", menuName = "ScriptableObjects/List")]
public class TaskList : ScriptableObject
{
    public List<Task> taskList;
}
