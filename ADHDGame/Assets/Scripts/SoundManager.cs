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

    [Header("Music parts")]
    public List<AudioSource> music;

    int playingPart = 0;

    public static List<SoundAction> actions;

    void Awake()
    {
        instance = this;
        actions = new List<SoundAction>();
    }

    void Update()
    {
        foreach (SoundAction action in actions)
        {
            Debug.Log(action.ToString());
            switch (action)
            {
                case SoundAction.click:
                    PlayClick();
                    break;
                case SoundAction.message:
                    PlayMessage();
                    break;
                case SoundAction.score:
                    PlayScore();
                    break;
                case SoundAction.thought:
                    PlayThogught();
                    break;
                case SoundAction.openFirstMessage:
                    MusicNextPart();
                    break;
                case SoundAction.drinkWaterMessage:
                    MusicNextPart();
                    break;
                case SoundAction.drinkWater:
                    MusicNextPart();
                    break;
            }
        }

        actions = new List<SoundAction>();
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

    public void PlayMusic()
    {
        playingPart = 0;
        music[playingPart].Play();
    }

    public void StopAllMusic()
    {
        music[playingPart].Stop();
    }

    public void MusicNextPart()
    {
        music[playingPart].Stop();
        playingPart = (playingPart + 1) % music.Count;
        music[playingPart].Play();
    }

    public static void RegisterAction(SoundAction action)
    {
        actions.Add(action);
    }

    public enum SoundAction
    {
        click,
        message,
        thought,
        score,
        openFirstMessage,
        drinkWaterMessage,
        drinkWater,
    }
}
