using System;
using DG.Tweening;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Utils
{
    /// <summary>
    /// This class responsible for shaking the game object for a m_DampingSpeed time. 
    /// </summary>
    public class ShakeBehavior : MonoBehaviour
    {
        // A measure of magnitude for the shake. Tweak based on your preference
        [SerializeField] float m_ShakeStength = 1f;
        // A measure of how quickly the shake effect should evaporate
        [SerializeField] float m_ShakeSpeed = 2f;

        private IShakeTweener m_ShakeTweener;

        public void Awake()
        {
            m_ShakeTweener = new DoTweenShake();
        }

        /// <summary>
        /// Trigger the shake behaviour
        /// </summary>
        public void Shake()
        {
            m_ShakeTweener.Shake(transform, m_ShakeSpeed, m_ShakeStength);
        }
    }
}