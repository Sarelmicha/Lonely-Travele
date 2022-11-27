using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Core
{
    /// <summary>
    /// This class responsible for managing the level (Start level process, Restart level process)
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelLightManager m_LevelLightManager;
        [SerializeField] private CameraSwitcher m_CameraSwitcher;

        /// <summary>
        /// Invoke when the level should start inorder to notify any component needed to restart.
        /// </summary>
        public event Action<bool> OnLevelShouldRestart;
        
        /// <summary>
        /// Invoke when the level started in order to notify any component needed that level was started.
        /// </summary>
        public event Action OnLevelStarted;
        
        /// <summary>
        /// Invoke when the state should be saved in order to notify any component needed to save its state.
        /// </summary>
        public event Action OnStateShouldBeSaved;
        
        private Animator m_Transitions;
        private bool m_ShouldExecuteFullRestart;
        private List<Coroutine> m_RunningCoroutines;

        /// <summary>
        /// Start restarting the level process.
        /// </summary>
        /// <param name="shouldExecuteFullRestart">A flag that indicate whether to execute a full restart</param>
        public void StartRestartLevel(bool shouldExecuteFullRestart)
        {
            m_ShouldExecuteFullRestart = shouldExecuteFullRestart;
            m_Transitions.SetTrigger("Flash");
        }

        /// <summary>
        /// Restart the level.
        /// Called from the animation key frame in the editor.
        /// </summary>
        public void RestartLevel()
        {
            StartCoroutine(m_LevelLightManager.DarkenLevel(m_ShouldExecuteFullRestart));
            OnLevelShouldRestart?.Invoke(m_ShouldExecuteFullRestart);
            m_CameraSwitcher.Reset(m_ShouldExecuteFullRestart);
           
            StopRunningRoutines();
            
            if (m_ShouldExecuteFullRestart)
            {
                ExposeLevel();
            }
        }

        /// <summary>
        /// Notify that the current state of the level should be saved.
        /// </summary>
        public void SaveState()
        {
            OnStateShouldBeSaved?.Invoke();
        }

        private void Awake()
        {
            m_Transitions = GetComponent<Animator>();
            m_RunningCoroutines = new List<Coroutine>();
        }

        private void Start()
        {
            ExposeLevel();
        }

        private void StopRunningRoutines()
        {
            foreach (var runningCoroutine in m_RunningCoroutines.Where(runningCoroutine => runningCoroutine != null))
            {
                StopCoroutine(runningCoroutine);
            }

            m_RunningCoroutines.Clear();
        }
        
        private void ExposeLevel()
        {
            IlluminateLevel(false, () =>
            {
                m_CameraSwitcher.SwitchCamera(CameraType.ExposureCamera, OnLevelExposed);
            });
        }

        private void IlluminateLevel(bool immediately = false, Action onComplete = null)
        {
            Coroutine illuminateCoroutine = null;

            illuminateCoroutine = StartCoroutine(m_LevelLightManager.IlluminateLevel(immediately, () =>
            {
                if (m_RunningCoroutines.Contains(illuminateCoroutine))
                {
                    m_RunningCoroutines.Remove(illuminateCoroutine);
                }

                onComplete?.Invoke();
            }));

            m_RunningCoroutines.Add(illuminateCoroutine);
        }

        private void DarkenLevel(bool immediately = false, Action onComplete = null)
        {
            Coroutine darkenCoroutine = null;

            darkenCoroutine = StartCoroutine(m_LevelLightManager.DarkenLevel(immediately, () =>
            {
                if (m_RunningCoroutines.Contains(darkenCoroutine))
                {
                    m_RunningCoroutines.Remove(darkenCoroutine);
                }

                onComplete?.Invoke();
            }));

            m_RunningCoroutines.Add(darkenCoroutine);
        }

        private void OnLevelExposed()
        {
            m_CameraSwitcher.SwitchCamera(CameraType.PlayableCamera, () =>
            {
                DarkenLevel(false, OnLevelStarted);
            });
        }

        public void FinishLevel()
        {
            // Level Finished...
        }
    }
}