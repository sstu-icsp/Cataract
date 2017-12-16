using UnityEngine;

public class AudioManager : Element
{

    public float minPitch;
    public float maxPitch;
    public float EfxVolume
    {
        get { return efxVolume; }
        set { value = Mathf.Clamp01(value); efxVolume = value; foreach (AudioSource src in efxSources) src.volume = value; }
    }
    public float MusicVolume
    {
        get { return musicVolume; }
        set { value = Mathf.Clamp01(value); musicVolume = value; musicSource.volume = value;}
    }
    public static AudioManager instance;
    public AudioClip music;
    private AudioSource efxSource;
    private int lastEfxIndex;
    private int size;
    private PitchController controller;
    private float efxVolume = 1f;
    private float musicVolume = 1f;
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource[] efxSources;

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
    }

    public void PlayEffect(AudioClip clip)
    {
        efxSource = getEfxSource();
        efxSource.pitch = 1;
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

    public void PlayAtPitch(AudioClip clip, float pitch)
    {
        efxSource = getEfxSource();
        controller.PlayAtPitch(clip, efxSource, pitch);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ToggleMusicMute()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleEffectsMute()
    {
        foreach(AudioSource src in efxSources)
            src.mute = !src.mute;
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

    public void StopAllSounds()
    {
        foreach (AudioSource a in efxSources)
        {
            a.Stop();
        }
    }
}