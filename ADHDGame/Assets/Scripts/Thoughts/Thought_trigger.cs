using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Thought_trigger : MonoBehaviour

{
    [SerializeField]
    thought_Transform thought_Transform;

    private void OnTriggerEnter2D (Collider2D other)
    {


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
}

