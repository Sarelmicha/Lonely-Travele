using UnityEngine;
using Rigidbody2D = UnityEngine.Rigidbody2D;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for control the logic and UI of the player.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float m_Thrust;
        [SerializeField] private Slingshot m_Slingshot;
        
        private Rigidbody2D m_Rigidbody;
        private PlayerControllerLogic m_PlayerControllerLogic;
        private PlayerSpotlight m_PlayerSpotlight;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_PlayerSpotlight = GetComponentInChildren<PlayerSpotlight>();
            m_PlayerControllerLogic = new PlayerControllerLogic(m_Rigidbody, m_Thrust, transform.position, m_PlayerSpotlight);
        }
        
        private void Start()
        {
            m_Slingshot.SubscribeOnTargetReleasedEvent(m_PlayerControllerLogic.Jump);
            m_PlayerSpotlight.SubscribeOnLightReachedZeroEvent(m_PlayerControllerLogic.Reset);
        }

        private void OnDestroy()
        {
            m_Slingshot.UnsubscribeOnTargetReleasedEvent(Jump);
            m_PlayerSpotlight.UnsubscribeOnLightReachedZeroEvent(Die);
        }
        
        /// <summary>
        /// Start die process of the player
        /// </summary>
        public void Die()
        {
            //Do some die animation
            m_PlayerControllerLogic.Reset();
        }

        /// <summary>
        /// Reset the player position in the initial position and set it velocity to zero
        /// </summary>
        public void Reset()
        {
            m_PlayerControllerLogic.Reset();
            //Do some awake animation
        }

        /// <summary>
        /// Set the initial position of the player.
        /// </summary>
        /// <param name="position">The position to set as initial position</param>
        public void SetInitialPosition(Vector3 position)
        {
            m_PlayerControllerLogic.InitialPosition = position;
        }

        /// <summary>
        /// Apply force to the player in a certain vector
        /// </summary>
        /// <param name="force">The force vector to add</param>
        public void AddForce(Vector2 force)
        {
            m_PlayerControllerLogic.AddForce(force);
        }

        /// <summary>
        /// Increasing the light radius of the player
        /// </summary>
        /// <param name="value">The value to increase from the light radius</param>
        public void IncreaseLight(float value)
        {
            m_PlayerSpotlight.IncreaseLight(value);
        }

        /// <summary>
        /// Reduce the light radius of the player
        /// </summary>
        /// <param name="lightToReduce">The value to reduce from the light radius</param>
        public void ReduceLight(float value)
        {
            m_PlayerSpotlight.ReduceLight(value);
        }
        
        private void Jump(Vector3 position)
        {
            m_PlayerControllerLogic.Jump(position);
        }
    }
}

