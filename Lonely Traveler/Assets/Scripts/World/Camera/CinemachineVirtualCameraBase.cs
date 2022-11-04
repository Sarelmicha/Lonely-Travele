using System;
using Cinemachine;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.Camera
{
    /// <summary>
    /// Define thw functionality of the Camera.
    /// </summary>
    public abstract class CinemachineVirtualCameraBase : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera m_VirtualCamera;

        public void SetAsMainCamera(Action onComplete = null)
        {
            m_VirtualCamera.Priority = 10;
            OnSetToMainCamera(onComplete);
        }

        public void RemoveAsMainCamera()
        {
            m_VirtualCamera.Priority = 0;
        }

        /// <summary>
        /// Call whenever the camera was switched to main camera.
        /// </summary>
        /// <param name="onComplete"></param>
        protected abstract void OnSetToMainCamera(Action onComplete = null);

        /// <summary>
        /// Reset the <see cref="CinemachineVirtualCameraBase"/> components.
        /// </summary>
        /// <param name="shouldFullReset">A flag that indicate whether should we full reset the strategy</param>
        public abstract void Reset(bool shouldFullReset);
    }
}