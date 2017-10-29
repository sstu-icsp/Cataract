using System;
using UnityEngine;

class RhythmGController : FightGameplay
{

    private float[] beatSeqence = {0.5f, 0.25f, 0.5f, 0.25f, 0.5f };
    private float totalTime;
    private float[] beatAnswerSequence;
    private int currBeatInd;
    private float currTime, answerTime;
    private AndroidJavaObject vibrator;
    bool vibrated;
    private int state;

    public override void Init()
    {
        currBeatInd = 0;
        currTime = beatSeqence[0];
        state = 0;
        beatAnswerSequence = new float[beatSeqence.Length];
        for (int i = 0; i < beatSeqence.Length; i++)
            totalTime += beatSeqence[i];
        AudioManager.instance.PlayAtPitch(AudioManager.instance.beat, UnityEngine.Random.Range(0.25f, 1));
    }

    public override void UpdateGameplay()
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
                        AudioManager.instance.PlayAtPitch(AudioManager.instance.beat, UnityEngine.Random.Range(0.25f, 1));
                        currBeatInd = 0;
                        currTime = 0;
                        answerTime = 0;
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
        if (currTime <= 0)
        {
            AudioManager.instance.PlayAtPitch(AudioManager.instance.beat, UnityEngine.Random.Range(0.25f, 1));
            if (currBeatInd + 1 < beatSeqence.Length)
                currTime = beatSeqence[++currBeatInd];
            vibrated = false;
        }
        currTime -= Time.unscaledDeltaTime;
    }

    private void RecordPlayerTaps()
    {
        currTime += Time.unscaledDeltaTime;
        answerTime += Time.unscaledDeltaTime;
        if (Input.GetMouseButtonDown(0)){
            AudioManager.instance.PlayAtPitch(AudioManager.instance.beat, UnityEngine.Random.Range(0.25f, 1));
            beatAnswerSequence[currBeatInd++] = currTime;
            currTime = 0;
            if(currBeatInd == beatSeqence.Length - 1)
                state = 3;
        }
        if(answerTime > totalTime)
        {
            for (int i = currBeatInd; i < beatAnswerSequence.Length; i++)
                beatAnswerSequence[i] = 0;
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
        float delta = 0;

        for (int i = 0; i < beatSeqence.Length; i++) {
            delta += Math.Abs(beatSeqence[i] - beatAnswerSequence[i]);
        }
        float ratio = (float)delta / totalTime;
        return (int)(-ratio * 30 + 10);//interval [-20, 10]
    }


    protected override void Finish(int result)
    {
        base.Finish(result);
    }
}

