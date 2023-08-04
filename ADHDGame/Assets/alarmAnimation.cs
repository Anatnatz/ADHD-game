using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarmAnimation : MonoBehaviour
{
    public float speed;
    Vector3 rotataionRight;
    Vector3 rotataionLeft;

    void Start()
    {
        rotataionRight = new Vector3(0, 0, -15f);
        rotataionLeft = new Vector3(0, 0, 15);
        StartCoroutine(PhoneVibration());
    }
    public IEnumerator PhoneVibration()
    {
        while (transform.rotation.z > rotataionRight.z)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotataionRight), 3f * speed * Time.deltaTime);
            yield return null;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotataionLeft), speed * Time.deltaTime);
            yield return null;
        }

    }
}
