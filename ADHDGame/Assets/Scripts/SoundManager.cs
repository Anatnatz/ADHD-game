using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource score;
    public AudioSource thought;
    public AudioSource message;
    public AudioSource click;

    void Awake()
    {
        instance = this;
    }

    public void PlayScore()
    {
        score.Play();
    }

    public void PlayThogught()
    {
        thought.Play();
    }

    public void PlayMessage()
    {
        message.Play();
    }

    public void PlayClick()
    {
        click.Play();
    }
}
