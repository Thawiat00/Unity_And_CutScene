using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChoiceCutsceneState : IChoiceDialogState
{
    private ChoiceDialogManager manager;
    private string clipName;
    private VideoPlayer videoPlayer;
    private RawImage rawImage;

    public ChoiceCutsceneState(ChoiceDialogManager manager, string clipName)
    {
        this.manager = manager;
        this.clipName = clipName;
        this.videoPlayer = manager.GetComponent<VideoPlayer>();
        this.rawImage = manager.GetRawImage();
    }

    public void Enter()
    {
        PlayVideo();
    }

    public void Update()
    {
        if (!videoPlayer.isPlaying || Input.GetKeyDown(KeyCode.Space))
        {
            manager.NextState();
        }
    }

    public void Exit()
    {
        videoPlayer.Stop();
        rawImage.gameObject.SetActive(false);
    }

    private void PlayVideo()
    {
        VideoClip clip = Resources.Load<VideoClip>("Videos/" + clipName);
        if (clip != null)
        {
            videoPlayer.clip = clip;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            rawImage.gameObject.SetActive(true);
            RenderTexture rt = new RenderTexture(1280, 720, 24);
            videoPlayer.targetTexture = rt;
            rawImage.texture = rt;
            videoPlayer.isLooping = false;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Video clip not found: " + clipName);
            manager.NextState();
        }
    }
}
