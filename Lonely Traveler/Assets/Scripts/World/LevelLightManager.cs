using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    /// <param name="onComplete"Invoke when the level illumination is finished</param>
    public IEnumerator DarkenLevel(Action onComplete = null)
    {
        while (m_LevelGlobalLight.intensity > m_MinimumLightLevel)
        {
            m_LevelGlobalLight.intensity -= m_LightReducerRate;
            yield return null;
        }
        
        onComplete?.Invoke();
    }

    /// <summary>
    /// Slowly illuminate the light level completely. 
    /// </summary>
    /// <param name="onComplete"Invoke when the level illumination is finished</param>
    public IEnumerator IlluminateLevel(Action onComplete = null)
    {
        while (m_LevelGlobalLight.intensity < m_MaximumLightLevel)
        {
            m_LevelGlobalLight.intensity += m_LightIncreaserRate;
            yield return null;
        }
        
        onComplete?.Invoke();
    }

    /// <summary>
    /// Reset the level light. 
    /// </summary>
    /// <param name="shouldFullReset">A flag that indicate whether should we full reset the strategy</param>
    public void Reset(bool shouldFullReset)
    {
        m_LevelGlobalLight.intensity = m_MinimumLightLevel;
    }
}
