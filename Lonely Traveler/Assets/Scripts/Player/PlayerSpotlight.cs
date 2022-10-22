using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace HappyFlow.LonelyTraveler.Player
{
    public class PlayerSpotlight : MonoBehaviour
    {
        [SerializeField] private float m_InitialSpotlightRadius;
        [SerializeField] private float m_IluminateSpotlightRate;
        [SerializeField] private Light2D m_Spotlight;
        private PlayerSpotlightLogic PlayerSpotlightLogic => m_PlayerSpotlightLogic ??= new PlayerSpotlightLogic(m_Spotlight, m_InitialSpotlightRadius);
        private PlayerSpotlightLogic m_PlayerSpotlightLogic;
        
        /// <summary>
        /// Increasing the light radius of the player
        /// </summary>
        /// <param name="value">The value to increase from the light radius</param>
        public void IncreaseLight(float value)
        {
            PlayerSpotlightLogic.IncreaseLight(value);
        }

        /// <summary>
        /// Reduce the light radius of the player
        /// </summary>
        /// <param name="lightToReduce">The value to reduce from the light radius</param>
        public void ReduceLight(float value)
        {
           PlayerSpotlightLogic.ReduceLight(value);
        }

        /// <summary>
        /// Reset the light value to be equal to the initial value
        /// </summary>
        public void Reset()
        {
            PlayerSpotlightLogic.Reset();
        }

        public void SubscribeOnLightReachedZeroEvent(Action action)
        {
            PlayerSpotlightLogic.OnLightReachedZero += action;
        }

        public void UnsubscribeOnLightReachedZeroEvent(Action action)
        {
            PlayerSpotlightLogic.OnLightReachedZero -= action;
        }

        /// <summary>
        /// Illuminate the spotlight to the initial light
        /// </summary>
        /// <param name="onComplete">Invoke when the spotlight finished illuminate</param>
        public IEnumerator IlluminateSpotlight(Action onComplete = null)
        {
            while (m_Spotlight.pointLightOuterRadius < m_InitialSpotlightRadius)
            {
                PlayerSpotlightLogic.IncreaseLight(m_IluminateSpotlightRate);
                yield return null;
            }
            
            onComplete?.Invoke();
        }
    }
}
