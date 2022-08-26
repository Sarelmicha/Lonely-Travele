using System;
using UnityEngine.Rendering.Universal;

namespace HappyFlow.LonelyTraveler.Player
{
    public class PlayerSpotlightLogic
    {
        private readonly Light2D m_Spotlight;
        private readonly float m_InitialLight;
        public event Action OnLightReachedZero;

        public PlayerSpotlightLogic(Light2D spotlight, float initialLight)
        {
            m_Spotlight = spotlight;
            m_InitialLight = initialLight;
        }
        
        /// <summary>
        /// Increasing the light radius of the player
        /// </summary>
        /// <param name="value">The value to increase from the light radius</param>
        public void IncreaseLight(float value)
        {
            m_Spotlight.pointLightOuterRadius += value;
        }

        /// <summary>
        /// Reduce the light radius of the player
        /// </summary>
        /// <param name="lightToReduce">The value to reduce from the light radius</param>
        public void ReduceLight(float value)
        {
            m_Spotlight.pointLightOuterRadius -= value;

            if (m_Spotlight.pointLightOuterRadius <= 0)
            {
                OnLightReachedZero?.Invoke();
            }
        }

        /// <summary>
        /// Reset the light value to be equal to the initial value
        /// </summary>
        public void Reset()
        {
            m_Spotlight.pointLightOuterRadius = m_InitialLight;
        }
    }
}