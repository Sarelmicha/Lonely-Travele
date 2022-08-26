using UnityEngine;

namespace HappyFlow.LonelyTraveler.Utils
{
    /// <summary>
    /// This class responsible for shaking the game object for a m_DampingSpeed time. 
    /// </summary>
    public class ShakeBehavior : MonoBehaviour
    {
        // Desired duration of the shake effect
        float m_ShakeDuration = 0f;

        // A measure of magnitude for the shake. Tweak based on your preference
        [SerializeField] float m_ShakeMagnitude = 1f;

        // A measure of how quickly the shake effect should evaporate
        [SerializeField] float m_DampingSpeed = 2f;

        // The initial position of the GameObject
        Vector3 initialPosition;

        /// <summary>
        /// Trigger the shake behaviour
        /// </summary>
        public void TriggerShake()
        {
            m_ShakeDuration = 1.0f;
        }
        
        private void OnEnable()
        {
            initialPosition = transform.localPosition;
        }

        private void Update()
        {
            if (m_ShakeDuration == 0)
            {
                return;
            }

            if (m_ShakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * m_ShakeMagnitude;

                m_ShakeDuration -= Time.deltaTime * m_DampingSpeed;
            }
            else
            {
                m_ShakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }
}