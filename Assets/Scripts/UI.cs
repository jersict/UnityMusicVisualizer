using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    public static UI ui;
    public Slider vol;
    public Slider pos;
    // Start is called before the first frame update
    void Start()
    {
        ui = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (MusicPlayer.player)
        {
            pos.value = MusicPlayer.player.GetMusicPosition();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPlayPause();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vol.value += 0.05f;
            MusicPlayer.player.VolUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vol.value -= 0.05f;
            MusicPlayer.player.VolDown();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MusicPlayer.player.SkipForward();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MusicPlayer.player.SkipBack();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MusicPlayer.player.MuteMusic();
        }



    }

    public void OnPositionValueChange(BaseEventData eventData)
    {
        MusicPlayer.player.SetMusicPosition(pos.value);
    }
    public void OnVolumeValueChange(BaseEventData eventData)
    {
        MusicPlayer.player.SetVolume(vol.value);
    }

    public void OnPlay(BaseEventData eventData)
    {
        if (!MusicPlayer.player.IsPlaying()) {
            MusicPlayer.player.PlayMusic();
        }
    }
    public void OnPause(BaseEventData eventData)
    {
        if (MusicPlayer.player.IsPlaying())
        {
            MusicPlayer.player.PauseMusic();
        }
    }
    public void OnStop(BaseEventData eventData)
    {
        if (MusicPlayer.player.IsPlaying())
        {
            MusicPlayer.player.StopMusic();
        }
    }
    public void OnMute(BaseEventData eventData)
    {
        if (MusicPlayer.player.IsPlaying())
        {
            MusicPlayer.player.MuteMusic();
        }
    }
    public void OnPlayPause()
    {
        if (MusicPlayer.player.IsPlaying())
        {
            MusicPlayer.player.PauseMusic();
        }
        else
        {
            MusicPlayer.player.PlayMusic();
        }
    }
    public void onQuit(BaseEventData eventData)
    {

        Application.Quit();

    }
}