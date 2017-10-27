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
    private Beat[] beatAnswerSequence;
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
    }

    public override void Init()
    {
        currBeatInd = 0;
        currTime = beatSeqence[0].Duration;
        state = 0;
        beatAnswerSequence = new Beat[beatSeqence.Length];
    }

    public override void Update()
    {
        switch (state)
        {
            case 0: //playing beat sequence
                {
                    PlayBeatSequence();
                    if (currBeatInd == beatSeqence.Length - 1)
                        state = 1;
                    break;
                }
            case 1: //waiting for player tap 
                {
                    if(Input.GetMouseButton(0))
                    {
                        PitchController.instance.StartPlaying(1);
                        currBeatInd = 0;
                        currTime = 0;
                        state = 2;
                    }
                    
                    break;
                }
            case 2: //user input
                {
                    RecordPlayerTaps();
                    break;
                }
            case 3: //calculate score based on taps and finish
                {
                    int score = CalculateScore();
                    Finish(score);
                    break;
                }
        }
    }

    private void PlayBeatSequence()
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
    }

    private void RecordPlayerTaps()
    {
        currTime += Time.unscaledDeltaTime;
        if (Input.GetMouseButtonDown(0)){
            PitchController.instance.StartPlaying(1);
            beatAnswerSequence[currBeatInd++] = new Beat(false, (int)(currTime * 1000));
        }
        if (Input.GetMouseButtonUp(0))
        {
            PitchController.instance.StopPlaying();
            beatAnswerSequence[currBeatInd++] = new Beat(true, (int)(currTime * 1000));
            currTime = 0;
            if(currBeatInd == beatSeqence.Length - 1)
                state = 3;
        }
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
    }
    private int CalculateScore()
    {
        int delta = 0, total = 4500;

        for (int i = 0; i < beatSeqence.Length; i++) {
            delta += Math.Abs(beatSeqence[i].Duration - beatAnswerSequence[i].Duration);
        }
        return (int)(20 * (delta / total - 0.5));
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