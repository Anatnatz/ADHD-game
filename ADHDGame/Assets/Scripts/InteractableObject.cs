using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskList", menuName = "ScriptableObjects/Object")]
public class InteractableObject : ScriptableObject
{
    public List<RelatedTask> relatedTasks;

    [Serializable]
    public class RelatedTask
    {
        public Task task;

        public string taskName = "task name";

        public Task_Enum taskType;

        public string textInApp;

        public float duration = 3f;

        public float waitingTime = 0f;

        public int score = 20;

        public bool isDone = false;

        // public Status_Enum status;
        public Animation animation;

        public Task waitingOnTask;

        public Thought_Enum blockingThought;

        public RelatedTask(
            string _taskName,
            Task_Enum _taskType,
            string _textInApp,
            float _duration,
            float _waitingTime,
            int _score,
            bool _isDone,
            Task _waitingOnTask,
            Thought_Enum _blockingThought
        )
        {
            task.taskName = _taskName;
            task.taskType = _taskType;
            task.textInApp = _textInApp;
            task.duration = _duration;
            task.waitingTime = _waitingTime;
            task.score = _score;
            task.isDone = _isDone;
            task.waitingOnTask = _waitingOnTask;
            task.blockingThought = _blockingThought;
        }
    }
}
