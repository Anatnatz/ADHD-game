using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Thought_trigger : MonoBehaviour
{
    [SerializeField]
    thought_Transform thought_Transform;

    public static Thought_trigger instance;

    void Awake()
    {
        instance = this;
    }

    public void TriggerThought(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.tag == "taskApp")
        {
            Debug.Log("push to taskApp ");
            thought_Transform.thoughtTransformStatus = ThoughtStatus.PushToApp;
            thought_Transform.changeStatuse(ThoughtStatus.PushToApp);
            thought_Transform.pushToApp();
            thought_Transform.updateNumOfAppearanceOnApp();
            Destroy(thought_Transform.gameObject);
            Destroy(this);
        }

        if (other.tag == "border")
        {
            Debug.Log("ignore task ");
            thought_Transform.thoughtTransformStatus = ThoughtStatus.Deleted;
            thought_Transform.changeStatuse(ThoughtStatus.Deleted);
            Destroy(thought_Transform.gameObject);
            Destroy(this);
        }
    }
}
