using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using HappyFlow.LonelyTraveler.World.LevelExposure;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera m_ExposureCamera;
   [SerializeField] private CinemachineVirtualCamera m_PlayableCamera;

   private List<CinemachineVirtualCamera> m_Cameras;

   private void Awake()
   {
      m_Cameras = new List<CinemachineVirtualCamera>();
      Register(m_ExposureCamera);
      Register(m_PlayableCamera);
   }

   private CinemachineVirtualCamera m_ActiveCamera;

   private bool IsActiveCamera(CinemachineVirtualCamera camera)
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

      camera.Priority = 10;
      m_ActiveCamera = camera;

      foreach (var virtualCamera in m_Cameras.Where(virtualCamera => virtualCamera != camera))
      {
         virtualCamera.Priority = 0;
      }

      var levelExposureStrategy = camera.GetComponent<LevelExposureStrategy>();
      
      if (levelExposureStrategy == null)
      {
         return;
      }
      
      levelExposureStrategy.Expose(onComplete);
   }

   private void Register(CinemachineVirtualCamera camera)
   {
      if (m_Cameras.Contains(camera))
      {
         Debug.Log($"Camera is already registered = {camera} ");
         return;
      }
      
      m_Cameras.Add(camera);
      Debug.Log($"Camera registered = {camera} ");
   }
   
   private void Unregister(CinemachineVirtualCamera camera)
   {
      if (!m_Cameras.Contains(camera))
      {
         Debug.Log($"Camera is not registered = {camera} ");
         return;
      }
      
      m_Cameras.Remove(camera);
      Debug.Log($"Camera unregister = {camera} ");
   }

   public enum CameraType
   {
      ExposureCamera,
      PlayableCamera
   }

   public void Reset(bool shouldFullReset)
   {
      var levelExposureStrategy = m_ActiveCamera.GetComponent<LevelExposureStrategy>();

      if (levelExposureStrategy == null)
      {
         return;
      }
      
      levelExposureStrategy.Reset(shouldFullReset);

   }
}
