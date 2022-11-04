using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using HappyFlow.LonelyTraveler.World.LevelExposure;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.Camera
{
   public class CameraSwitcher : MonoBehaviour
   {
      [SerializeField] private CinemachineVirtualCameraBase m_ExposureCamera;
      [SerializeField] private CinemachineVirtualCameraBase m_PlayableCamera;

      private List<CinemachineVirtualCameraBase> m_Cameras;

      private void Awake()
      {
         m_Cameras = new List<CinemachineVirtualCameraBase>();
         Register(m_ExposureCamera);
         Register(m_PlayableCamera);
      }

      private CinemachineVirtualCameraBase m_ActiveCamera;

      private bool IsActiveCamera(CinemachineVirtualCameraBase camera)
      {
         return camera == m_ActiveCamera;
      }

      public void SwitchCamera(CameraType cameraType, Action onComplete = null)
      {
         var camera = cameraType switch
         {
            CameraType.ExposureCamera => m_ExposureCamera,
            CameraType.PlayableCamera => m_PlayableCamera,
            _ => throw new ArgumentOutOfRangeException(nameof(cameraType), cameraType, null)
         };

         if (IsActiveCamera(camera))
         {
            onComplete?.Invoke();
            return;
         }

         m_ActiveCamera = camera;

         foreach (var virtualCamera in m_Cameras.Where(virtualCamera => virtualCamera != camera))
         {
            virtualCamera.RemoveAsMainCamera();
         }
         
         camera.SetAsMainCamera(onComplete);
      }

      private void Register(CinemachineVirtualCameraBase camera)
      {
         if (m_Cameras.Contains(camera))
         {
            Debug.Log($"Camera is already registered = {camera} ");
            return;
         }

         m_Cameras.Add(camera);
         Debug.Log($"Camera registered = {camera} ");
      }

      private void Unregister(CinemachineVirtualCameraBase camera)
      {
         if (!m_Cameras.Contains(camera))
         {
            Debug.Log($"Camera is not registered = {camera} ");
            return;
         }

         m_Cameras.Remove(camera);
         Debug.Log($"Camera unregister = {camera} ");
      }
      
      public void Reset(bool shouldFullReset)
      {
         m_ActiveCamera.Reset(shouldFullReset);
      }
   }
}