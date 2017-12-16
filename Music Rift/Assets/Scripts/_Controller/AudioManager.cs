using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Element
{

    public float minPitch;
    public float maxPitch;

    public AudioSource musicSource;
    public AudioSource[] efxSources;
    public static AudioManager instance;

    public AudioClip beat;

    private AudioSource efxSource;
    private int lastEfxIndex;
    private int size;
    private PitchController controller;
    float volume = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            size = efxSources.Length;
            controller = app.controller.pitch;
        }
        else if (instance != this)
            Destroy(gameObject);
        efxSource = getEfxSource();
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
        }
        if (volume == 0)
        {
            AudioListener.volume = 0;
        }
        else AudioListener.volume = 1;
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
        }
        if (volume == 0)
        {
            AudioListener.volume = 0;
        }
    }
    public void PlayEffect(AudioClip clip)
    {
        efxSource = getEfxSource();
        efxSource.pitch = 1;
        efxSource.volume = 1;
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayRandomSfx(params AudioClip[] clips)
    {
        int rIndex = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(minPitch, maxPitch);
        efxSource = getEfxSource();
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[rIndex];
            efxSource.Play();
    }

    internal void PlayAtPitch(AudioClip beat, float v)
    {
        efxSource = getEfxSource();
        controller.PlayAtPitch(beat, efxSource, v);
    }

    public void SetMusic(AudioClip clip)
    {

        if (musicSource.clip == null || musicSource.clip != null && musicSource.clip.name != clip.name)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }

    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    //Returns last AudioSource, that is not used to play a clip, or zero element of the array, if all sources are used
    private AudioSource getEfxSource()
    {
        if (lastEfxIndex < size - 1)
            return efxSources[lastEfxIndex++];
        else
        {
            lastEfxIndex = 0;
            return efxSources[lastEfxIndex];
        }
    }

    internal void StopSounds()
    {
        foreach (AudioSource a in efxSources)
        {
            a.Stop();
        }
    }
}