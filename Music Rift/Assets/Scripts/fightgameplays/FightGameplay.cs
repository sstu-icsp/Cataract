using System;
using UnityEngine;
/// <summary>
/// Superclass for all fight gameplays.
/// </summary>
public abstract class FightGameplay
{
    /// <summary>
    /// Used to re-initialize gameplay on startup
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// Used to perform per-frame operations, should be calling Finish(int)
    /// </summary>
    public abstract void Update();
    public delegate void GFinishedCallback(int result);
    public static event GFinishedCallback OnGFinished;

    /// <summary>
    /// Used to send result to all listeners of event OnGFinished
    /// to add functionality, override it AND use base.Finish(int)
    /// </summary>
    /// <param name="result">estimate of player performance (positive or negative)</param>
    protected virtual void Finish(int result) {
        if (OnGFinished != null)
            OnGFinished(result);
    }
      
}
