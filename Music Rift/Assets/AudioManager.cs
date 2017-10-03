using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public float minPitch;
    public float maxPitch;

    public AudioSource musicSource;
    public AudioSource[] efxSources;
    private AudioSource efxSource;
    private int lastEfxIndex;
    private int size;


    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            size = efxSources.Length;
        }
        else if (instance != this)
            Destroy(gameObject);
    }
    public void PlayEffect(AudioClip clip)
    {
        efxSource = getEfxSource();
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayRandomSfx(params AudioClip[] clips)
    {
        int rIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(minPitch, maxPitch);
        efxSource = getEfxSource();
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[rIndex];
        efxSource.Play();
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