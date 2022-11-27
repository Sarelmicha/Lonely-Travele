using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace HappyFlow.LonelyTraveler.Core
{
    public class LevelLightManager : MonoBehaviour
    {
        [SerializeField] private float m_MaximumLightLevel;
        [SerializeField] private float m_MinimumLightLevel;
        [SerializeField] private float m_LightReducerRate;
        [SerializeField] private float m_LightIncreaserRate;
        [SerializeField] private Light2D m_LevelGlobalLight;

        /// <summary>
        /// Darken the light level completely. 
        /// </summary>
        /// <param name="immediately">A flag that indicate whether the level should darken immediately or not.</param>
        /// <param name="onComplete"Invoke when the level illumination is finished</param>
        public IEnumerator DarkenLevel(bool immediately = false, Action onComplete = null)
        {
            if (immediately)
            {
                m_LevelGlobalLight.intensity = m_MinimumLightLevel;
                onComplete?.Invoke();
                yield break;
            }

            while (m_LevelGlobalLight.intensity > m_MinimumLightLevel)
            {
                m_LevelGlobalLight.intensity -= m_LightReducerRate * Time.deltaTime;
                yield return null;
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// Slowly illuminate the light level completely. 
        /// </summary>
        /// <param name="immediately">A flag that indicate whether the level should illuminate immediately or not.</param>
        /// <param name="onComplete"Invoke when the level illumination is finished</param>
        public IEnumerator IlluminateLevel(bool immediately = false, Action onComplete = null)
        {
            if (immediately)
            {
                m_LevelGlobalLight.intensity = m_MaximumLightLevel;
                onComplete?.Invoke();
                yield break;
            }

            while (m_LevelGlobalLight.intensity < m_MaximumLightLevel)
            {
                m_LevelGlobalLight.intensity += m_LightIncreaserRate * Time.deltaTime;
                yield return null;
            }

            onComplete?.Invoke();
        }
    }
}