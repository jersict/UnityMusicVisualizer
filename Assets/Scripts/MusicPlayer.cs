using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using UnityEditor;

public class MusicPlayer : MonoBehaviour
{
    string path;
    string extension;
    public AudioImporter importer;

    public static MusicPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = this;
    }

  
    public float GetMusicPosition() 
    {
        var music = GetComponent<AudioSource>();
        return Mathf.Clamp(music.time / music.clip.length, 0f, 1f);
    }
    public void SetMusicPosition(float pos)
    {
        var music = GetComponent<AudioSource>();
        music.time = (music.clip.length - 0.1f) * pos;
    }

    public void SetVolume(float vol)
    {
        var music = GetComponent<AudioSource>();
        music.volume = vol;
    }

    public void PauseMusic()
    {
        var music = GetComponent<AudioSource>();
        music.Pause();
    }
    
    public void StopMusic()
    {
        var music = GetComponent<AudioSource>();
        music.Stop();
    }

    public void MuteMusic()
    {
        var music = GetComponent<AudioSource>();
        music.mute = !music.mute;
    }

    public void PlayMusic()
    {
        var music = GetComponent<AudioSource>();
        music.Play();
    }
    public void VolUp()
    {
        var music = GetComponent<AudioSource>();
        music.volume += 0.05f;
    }
    public void VolDown()
    {
        var music = GetComponent<AudioSource>();
        music.volume -= 0.05f;
    }
    public void SkipForward()
    {
        var music = GetComponent<AudioSource>();
        music.time += 5;
    }
    public void SkipBack()
    {
        var music = GetComponent<AudioSource>();
        music.time -= 5;
    }
    public bool IsPlaying()
    {
        var music = GetComponent<AudioSource>();
        if (music && music.isPlaying)
        {
            return true;
        }
        return false;
    }

}
