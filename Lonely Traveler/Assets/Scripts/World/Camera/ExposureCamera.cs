using System;
using HappyFlow.LonelyTraveler.World.LevelExposure;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.Camera
{
    public class ExposureCamera : CinemachineVirtualCameraBase
    {
        [SerializeField] private LevelExposureStrategy m_LevelExposureStrategy;

        protected override void OnSetToMainCamera(Action onComplete = null)
        {
            m_LevelExposureStrategy.Expose(onComplete);
        }

        public override void Reset(bool shouldFullReset)
        {
            m_LevelExposureStrategy.Reset(shouldFullReset);
        }
    }
}