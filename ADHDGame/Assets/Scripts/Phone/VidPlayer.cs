using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    public static VidPlayer instance;

    [SerializeField]
    string videoFileName;

    VideoPlayer videoPlayer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetTiktokVideo();
    }

    public void ResetTiktokVideo()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            // videoPlayer.Play();
        }
    }

    public void PlayTikTokVideo()
    {
        videoPlayer.Play();
    }

    public void PauseTiktokVideo()
    {
        videoPlayer.Pause();
    }
}
