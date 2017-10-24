using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to play audio clip with shifting pitch
/// </summary>
public class PitchController : MonoBehaviour
{

    [Range(0.25f, 1)]
    public float pitchMultiplayer;
    public AudioSource source;

    public static PitchController instance;
    private float pitch;
    private float duration, t;
    private bool bFade;
    private enum State { fade_in, play_duration, play_to_pitch, fade_out }
    private State state;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        pitch = source.pitch;
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
                            gameObject.SetActive(false);
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
                        gameObject.SetActive(false);
                    }
                    break;
                }
            case State.play_to_pitch:
                {
                    source.pitch = pitch + pitchMultiplayer;
                    break;
                }
        }
          
    }

    /// <summary>
    /// Plays clip for specified duration with specidied 
    /// </summary>
    /// <param name="pitchMultiplayer">value in range [0.25, 1] used to scale down clip frequency</param>
    /// <param name="duration">duration of playback in seonds</param>
    /// <param name="fade">set to true if fade in and fade out are needed</param>
    public void PlayForDuration(float pitchMultiplayer, float duration, bool fade = false)
    {
        this.pitchMultiplayer = pitchMultiplayer;
        source.pitch = pitch + pitchMultiplayer;
        gameObject.SetActive(true);
        source.volume = 1;
        t = 0;
        this.duration = duration;
        if(!source.isPlaying)
        source.Play();
        bFade = fade;
        if (fade)
            state = State.fade_in;
        else
            state = State.play_duration;
    }

    /// <summary>
    /// Use StartPlaying(mult) to start continuous playing of a sound. PlayPitch(mult) to update pitch and StopPlaying() to finish playback.
    /// </summary>
    /// <param name="pitchMultiplayer">value in range [0.25, 1] used to scale down clip frequency</param>
    public void StartPlaying(float pitchMultiplayer)
    {
        gameObject.SetActive(true);
        source.volume = 1;
        source.Play();
        this.pitchMultiplayer = pitchMultiplayer;
        state = State.play_to_pitch;
    }
    /// <summary>
    /// Use StartPlaying(mult) to start continuous playing of a sound. PlayPitch(mult) to update pitch and StopPlaying() to finish playback.
    /// </summary>
    /// <param name="pitchMultiplayer">value in range [0.25, 1] used to scale down clip frequency</param>
    public void PlayPitch(float pitchMultiplayer)
    {
        this.pitchMultiplayer = pitchMultiplayer;
    }

    /// <summary>
    /// Use StartPlaying(mult) to start continuous playing of a sound. PlayPitch(mult) to update pitch and StopPlaying() to finish playback.
    /// </summary>
    public void StopPlaying()
    {
        gameObject.SetActive(false);
    }

}
