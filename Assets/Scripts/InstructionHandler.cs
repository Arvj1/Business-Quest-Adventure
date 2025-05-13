using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class InstructionHandler : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Image playPauseBtn;
    [SerializeField] Sprite playSprite, pauseSprite;
    public UnityEvent OnVideoSkipButtonPressed, OnInstructionSkipButtonPressed;

    private bool playToggle = false;
   public void PlayButtonAction()
   {
        playToggle = !playToggle;
        if (playToggle)
        {
            playPauseBtn.sprite = pauseSprite;
            videoPlayer.Play();
        }
        else
        {
            playPauseBtn.sprite = playSprite;
            videoPlayer.Pause();
        }
   }
    
    public void VideoSkipButtonAction()
    {
        videoPlayer.Pause();
        OnVideoSkipButtonPressed.Invoke();
    }

    public void InstructionSkipButtonAction()
    {
        OnInstructionSkipButtonPressed.Invoke();
    }
}
