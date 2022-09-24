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
        private Light2D m_Spotlight;
        private PlayerSpotlightLogic m_PlayerSpotlightLogic;

        private void Awake()
        {
            m_Spotlight = GetComponent<Light2D>();
            m_PlayerSpotlightLogic = new PlayerSpotlightLogic(m_Spotlight, m_InitialSpotlightRadius);
        }
        
        /// <summary>
        /// Increasing the light radius of the player
        /// </summary>
        /// <param name="value">The value to increase from the light radius</param>
        public void IncreaseLight(float value)
        {
            m_PlayerSpotlightLogic.IncreaseLight(value);
        }

        /// <summary>
        /// Reduce the light radius of the player
        /// </summary>
        /// <param name="lightToReduce">The value to reduce from the light radius</param>
        public void ReduceLight(float value)
        {
           m_PlayerSpotlightLogic.ReduceLight(value);
        }

        /// <summary>
        /// Reset the light value to be equal to the initial value
        /// </summary>
        public void Reset()
        {
            m_PlayerSpotlightLogic.Reset();
        }

        public void SubscribeOnLightReachedZeroEvent(Action action)
        {
            m_PlayerSpotlightLogic.OnLightReachedZero += action;
        }

        public void UnsubscribeOnLightReachedZeroEvent(Action action)
        {
            m_PlayerSpotlightLogic.OnLightReachedZero -= action;
        }

        /// <summary>
        /// Illuminate the spotlight to the initial light
        /// </summary>
        public IEnumerator IlluminateSpotlight()
        {
            while (m_Spotlight.pointLightOuterRadius < m_InitialSpotlightRadius)
            {
                m_PlayerSpotlightLogic.IncreaseLight(m_IluminateSpotlightRate);
                yield return null;
            }
        }
    }
}
