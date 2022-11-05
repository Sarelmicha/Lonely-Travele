using System;

namespace HappyFlow.LonelyTraveler.World.LevelExposure
{
    /// <summary>
    /// This class responsible for exposing the level to the user with a Skip Exposure Strategy.
    /// If this strategy will be used no exposure will be done and level will be start immediately . 
    /// </summary>
    public class SkipExposureStrategy : LevelExposureStrategy
    {
        public override void Expose(Action onComplete = null)
        {
            onComplete?.Invoke();
        }

        public override void Reset(bool shouldFullReset)
        {
            
        }
    }
}
