using System;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] bgmAudio;
    [SerializeField] private Sound[] sfxAudio;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public static AudioManager instance;


    //instance in order to load this script info from anywhere
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    //at start, play theme music
    private void Start()
    {
        PlayBGM("Sun");
    }



    //bgm (continuous) finding and playing logic
    public void PlayBGM(string title)
    {
        Sound sound = Array.Find(bgmAudio, specificSound => specificSound.audioName == title);
        if (sound == null)
        {
            Debug.Log("Track " + title + " is missing.");
        } else
        {
            bgmSource.clip = sound.audioClip;
            bgmSource.Play();
        }
    }


    //sfx (just one time) finding and playing logic
    public void PlaySFX (string title)
    {
        Sound sound = Array.Find(sfxAudio, specificSound => specificSound.audioName == title);
        if (sound == null)
        {
            Debug.Log("Track " + title + " is missing.");
        }
        else
        {
            sfxSource.PlayOneShot(sound.audioClip);
        }
    }

    public void StopMusic()
    {
        bgmSource.Stop();
    }
}

