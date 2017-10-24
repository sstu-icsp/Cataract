using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class RhythmGameplay : FightGameplay
{

    private Beat[] beatSeqence = { new Beat(true, 200), new Beat(false, 200), new Beat(true, 200), new Beat(false, 200), new Beat(true, 200), new Beat(false, 200),
    new Beat(true, 500), new Beat(false, 200), new Beat(true, 500), new Beat(false, 200), new Beat(true, 500), new Beat(false, 200),
        new Beat(true, 200), new Beat(false, 200), new Beat(true, 200), new Beat(false, 200), new Beat(true, 200), new Beat(false, 200) };

    private int currBeatInd;
    private float currTime;
    private AndroidJavaObject vibrator;
    bool vibrated;
    private int state;

    public void Awake()
    {
        if (Application.platform != RuntimePlatform.Android) return;
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        unityPlayer.Dispose();
        currentActivity.Dispose();
        state = 0;
    }

    public override void Init()
    {
        currBeatInd = 0;
        currTime = beatSeqence[0].Duration;
        state = 0;
    }

    public override void Update()
    {
        switch (state)
        {
            case 0: //playing beat sequence
                {
                    playBeatSequence();
                    break;
                }
            case 1: //waiting for player tap 
                {
                    /*
                    if(Input.touchCount > 0)
                    {
                       
                    }*/
                    state = 2;
                    break;
                }
            case 2: //user input
                {
                    recordPlayerTaps();
                    break;
                }
            case 3: //calculate score based on taps and finish
                {
                    Finish(0);
                    break;
                }
        }
    }

    private void recordPlayerTaps()
    {
        /*
        switch (Input.GetTouch(0).phase)
        {
            case TouchPhase.Began:
                {
                    Debug.Log("Began");
                    break;
                }
            case TouchPhase.Ended:
                {
                    Debug.Log("Ended");
                    break;
                }
        }*/
        state = 3;
    }

    private void playBeatSequence()
    {
        if (beatSeqence[currBeatInd].Vibrate && !vibrated)
        {
            if (Application.platform == RuntimePlatform.Android)
                vibrator.Call("vibrate", beatSeqence[currBeatInd].Duration);
            PitchController.instance.PlayForDuration(1, beatSeqence[currBeatInd].Duration / 1000f);
            // AudioManager.instance.PlayEffect()
            vibrated = true;
        }
        if (currTime <= 0)
        {

            if (currBeatInd + 1 < beatSeqence.Length)
                currTime = beatSeqence[++currBeatInd].Duration;
            vibrated = false;
        }
        currTime -= Time.unscaledDeltaTime * 1000;
        if (currBeatInd == beatSeqence.Length - 1)
            state = 1;
    }

    protected override void Finish(int result)
    {
        base.Finish(result);
    }

    struct Beat
    {
        private readonly bool vibrate;
        private readonly int duration;

        public Beat(bool vibrate, int duration)
        {
            this.vibrate = vibrate;
            this.duration = duration;
        }

        public bool Vibrate { get { return vibrate; } }
        public int Duration { get { return duration; } }
    }
}