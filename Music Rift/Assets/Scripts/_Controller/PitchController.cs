using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to play audio clip with shifting pitch
/// </summary>
public class PitchController : Element
{

    [Range(0.25f, 1)]
    public float pitchMultiplayer;
    public AudioSource source;
    private float pitch;
    private float duration, t;
    private bool bFade;
    private enum State { fade_in, play_duration, play_to_pitch, fade_out, idle }
    private State state;
  //  private float volume = 1f;// where is it use?

    void Start()
    {
        pitch = source.pitch;
        if (PlayerPrefs.HasKey("ToggleMusic"))
        {        
         //   volume = PlayerPrefs.GetFloat("ToggleMusic");//////??????? don't work


            /**
             * пока будет так 
             * */
            if (PlayerPrefs.GetFloat("ToggleMusic") == 0)
            {
                AudioListener.volume = 0;
            }else
            {
                AudioListener.volume = 1;
            }
        }

    }

    void Update()
    {
        switch (state)
        {
            case State.fade_in:
                {
                    t += Time.unscaledDeltaTime * 4;
                    source.volume = Mathf.Lerp(0, source.volume, t);
                    if (t >= 1)
                    {
                        t = 0;
                        state = State.play_duration;
                    }
                    break;
                }
            case State.play_duration:
                {
                    duration -= Time.unscaledDeltaTime;
                    if (duration <= 0)
                    {
                        if (bFade)
                            state = State.fade_out;
                        else
                        {
                            t = 0;
                            source.Stop();
                            state = State.idle;
                        }
                    }
                    break;
                }
            case State.fade_out:
                {
                    t += Time.unscaledDeltaTime * 4;
                    source.volume = Mathf.Lerp(source.volume, 0, t);
                    if (t >= 1)
                    {
                        t = 0;
                        source.Stop();
                        state = State.idle;
                    }
                    break;
                }
            case State.play_to_pitch:
                {
                    source.pitch = pitch + pitchMultiplayer;
                    break;
                }
            case State.idle:
                {
                    break;
                }
        }
          
    }

    /// <summary>
    /// Plays clip at pitch
    /// </summary>
    /// <param name="clip">clip for playback</param>
    /// <param name="source">source for playback</param>
    /// <param name="pitchMultiplayer">value in range [0.25, 1] used to scale down clip frequency</param>
    public void PlayAtPitch(AudioClip clip, AudioSource source, float pitchMultiplayer)
    {
        source.clip = clip;
        this.pitchMultiplayer = pitchMultiplayer;
        source.pitch = pitch + pitchMultiplayer;
        gameObject.SetActive(true);
        source.Play();
        state = State.idle;
    }

    /// <summary>
    /// Plays clip for specified duration at pitch 
    /// </summary>
    /// <param name="clip">clip for playback</param>
    /// <param name="source">source for playback</param>
    /// <param name="pitchMultiplayer">value in range [0.25, 1] used to scale down clip frequency</param>
    /// <param name="duration">duration of playback in seonds</param>
    /// <param name="fade">set to true if fade in and fade out are needed</param>
    public void Play4Duration(AudioClip clip, AudioSource source, float pitchMultiplayer, float duration, bool fade = false)
    {
        source.clip = clip;
        this.pitchMultiplayer = pitchMultiplayer;
        source.pitch = pitch + pitchMultiplayer;
        gameObject.SetActive(true);
            source.volume = 1;
        t = 0;
        this.duration = duration;
        if (!source.isPlaying)
            source.Play();
        bFade = fade;
        if (fade)
            state = State.fade_in;
        else
            state = State.play_duration;
    }

    /// <summary>
    /// Use StartPlaying(mult) to start continuous playing of a sound. SetPitch(mult) to update pitch and StopPlaying() to finish playback.
    /// </summary>
    /// <param name="clip">clip for playback</param>
    /// <param name="source">source for playback</param>
    /// <param name="pitchMultiplayer">value in range [0.25, 1] used to scale down clip frequency</param>
    public void StartPlaying(AudioClip clip, AudioSource source, float pitchMultiplayer)
    {
        source.clip = clip;
        gameObject.SetActive(true);
            source.volume = 1;
        source.Play();
        this.pitchMultiplayer = pitchMultiplayer;
        state = State.play_to_pitch;
    }
    /// <summary>
    /// Use StartPlaying(mult) to start continuous playing of a sound. SetPitch(mult) to update pitch and StopPlaying() to finish playback.
    /// </summary>
    /// <param name="pitchMultiplayer">value in range [0.25, 1] used to scale down clip frequency</param>
    public void SetPitch(float pitchMultiplayer)
    {
        this.pitchMultiplayer = pitchMultiplayer;
    }

    /// <summary>
    /// Use StartPlaying(mult) to start continuous playing of a sound. SetPitch(mult) to update pitch and StopPlaying() to finish playback.
    /// </summary>
    public void StopPlaying()
    {
        source.Stop();
        state = State.idle;
    }

}
