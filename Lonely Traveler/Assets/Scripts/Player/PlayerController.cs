using System;
using UnityEngine;
using Rigidbody2D = UnityEngine.Rigidbody2D;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for control the logic and UI of the player.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Invoked anytime the player died.
        /// </summary>
        public Action OnPlayerDied;
        
        [SerializeField] private float m_Thrust;
        [SerializeField] private Slingshot m_Slingshot;
        
        private Rigidbody2D m_Rigidbody;
        private PlayerControllerLogic m_PlayerControllerLogic;
        private PlayerSpotlight m_PlayerSpotlight;
        private LevelManager m_LevelManager;
        private bool m_CanJump;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_PlayerSpotlight = GetComponentInChildren<PlayerSpotlight>();
            m_PlayerControllerLogic = new PlayerControllerLogic(m_Rigidbody, m_Thrust, transform.position, m_PlayerSpotlight);
            
            var go = GameObject.FindGameObjectWithTag("LevelManager");

            if (go != null)
            {
                m_LevelManager = go.GetComponent<LevelManager>();
            }
        }
        
        private void Start()
        {
            m_Slingshot.SubscribeOnTargetReleasedEvent(Jump);
            m_PlayerSpotlight.SubscribeOnLightReachedZeroEvent(Die);
            m_LevelManager.OnLevelShouldRestart += Reset;
            m_LevelManager.OnLevelStarted += OnLevelStarted;
        }

        private void OnDestroy()
        {
            m_Slingshot.UnsubscribeOnTargetReleasedEvent(Jump);
            m_PlayerSpotlight.UnsubscribeOnLightReachedZeroEvent(Die);
            m_LevelManager.OnLevelShouldRestart -= Reset;
            m_LevelManager.OnLevelStarted -= OnLevelStarted;
        }
        
        /// <summary>
        /// Start die process of the player
        /// </summary>
        public void Die()
        {
            //Do some die animation
            m_LevelManager.StartRestartLevel();
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
        /// Illuminate the spotlight to the initial light
        /// </summary>
        /// <param name="onComplete">Invoke when the spotlight finished illuminate</param>
        private void IlluminateSpotlight(Action onComplete = null)
        {
            StartCoroutine(m_PlayerSpotlight.IlluminateSpotlight(onComplete));
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
            if (!m_CanJump)
            {
                return;
            }

            m_PlayerControllerLogic.Jump(position);
        }

        private void OnLevelStarted()
        {
            IlluminateSpotlight(EnableJump);
          
        }

        private void EnableJump()
        {
            m_CanJump = true;
        }

        private void DisableJump()
        {
            m_CanJump = false;
        }
    }
}

