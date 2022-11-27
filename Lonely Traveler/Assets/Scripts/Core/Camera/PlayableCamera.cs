using System;


namespace HappyFlow.LonelyTraveler.Core
{
    public class PlayableCamera : CinemachineVirtualCameraBase
    {
        protected override void OnSetToMainCamera(Action onComplete = null)
        {
           onComplete?.Invoke();
        }

        public override void Reset(bool shouldFullReset)
        {
            // For now, nothing to reset here...
        }
    }
}