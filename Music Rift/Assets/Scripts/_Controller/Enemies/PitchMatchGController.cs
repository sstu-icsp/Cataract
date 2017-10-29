class PitchMatchGController : FightGameplay
{

    private float targetPitch;
    private float currPitch;
    private PitchController controller;

    public override void Init()
    {
        // targetPitch = Random(0,25f, 1);
        // controller.StartPlaying(targetPitch );
    }

    public override void UpdateGameplay()
    {
       
    }

    protected override void Finish(int result)
    {
        base.Finish(result);
    }

}