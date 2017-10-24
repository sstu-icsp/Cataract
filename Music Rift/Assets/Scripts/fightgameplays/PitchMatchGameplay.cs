using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PitchMatchGameplay : FightGameplay
{

    private float targetPitch;
    private float currPitch;
    private PitchController controller;

    public override void Init()
    {
        // targetPitch = Random(0,25f, 1);
        // controller.StartPlaying(targetPitch );
    }

    public override void Update()
    {
       
    }

    protected override void Finish(int result)
    {
        base.Finish(result);
    }

}