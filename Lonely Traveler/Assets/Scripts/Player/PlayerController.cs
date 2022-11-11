using System;
using HappyFlow.LonelyTraveler.World;
using UnityEngine;
using Rigidbody2D = UnityEngine.Rigidbody2D;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for control the logic and UI of the player.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float m_Force;
        [SerializeField] private Slingshot m_Slingshot;
        
        private bool m_IsAlive;
        private bool m_CanJump;
        
        private Rigidbody2D m_Rigidbody;
        private PlayerControllerLogic m_PlayerControllerLogic;
        private PlayerSpotlight m_PlayerSpotlight;
        private PlayerCollider m_PlayerCollider;
        private LevelManager m_LevelManager;
        private TrajectoryPrediction m_TrajectoryPrediction;
        
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_TrajectoryPrediction = m_Slingshot.GetComponent<TrajectoryPrediction>();
            m_PlayerSpotlight = GetComponentInChildren<PlayerSpotlight>();
            m_PlayerCollider = GetComponentInChildren<PlayerCollider>();
            m_PlayerControllerLogic = new PlayerControllerLogic(m_Rigidbody, m_Force, transform.position, m_PlayerSpotlight);
            m_IsAlive = true;
            
            var go = GameObject.FindGameObjectWithTag("LevelManager");

            if (go != null)
            {
                m_LevelManager = go.GetComponent<LevelManager>();
            }

            SubscribeEvents();
        }
        
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            m_Slingshot.SubscribeOnTargetReleasedEvent(Jump);
            m_PlayerSpotlight.SubscribeOnLightReachedZeroEvent(Die);
            m_LevelManager.OnLevelShouldRestart += Reset;
            m_LevelManager.OnLevelStarted += OnLevelStarted;
            m_LevelManager.OnStateShouldBeSaved += SaveCurrentState;
            m_PlayerCollider.OnPlayerGrounded += EnableSlingshot;
            m_PlayerCollider.OnPlayerUngrounded += DisableSlingshot;
            m_TrajectoryPrediction.Initialize(m_Force);
        }

        private void UnsubscribeEvents()
        {
            m_Slingshot.UnsubscribeOnTargetReleasedEvent(Jump);
            m_PlayerSpotlight.UnsubscribeOnLightReachedZeroEvent(Die);
            m_LevelManager.OnLevelShouldRestart -= Reset;
            m_LevelManager.OnLevelStarted -= OnLevelStarted;
            m_LevelManager.OnStateShouldBeSaved -= SaveCurrentState;
            m_PlayerCollider.OnPlayerGrounded -= EnableSlingshot;
            m_PlayerCollider.OnPlayerUngrounded -= DisableSlingshot;
        }

        /// <summary>
        /// Start die process of the player
        /// </summary>
        public void Die()
        {
            if (!m_IsAlive)
            {
                return;
            }

            m_IsAlive = false;
            //Do some die animation
            m_LevelManager.StartRestartLevel(false);
        }

        /// <summary>
        /// Reset the player position in the initial position and set it velocity to zero
        /// </summary>
        /// <param name="shouldFullReset">A flag that indicate whether to full reset the player controller or not.</param>
        private void Reset(bool shouldFullReset)
        {
            m_PlayerControllerLogic.Reset(shouldFullReset);
            m_IsAlive = true;
            //Do some awake animation

            if (shouldFullReset)
            {
                m_CanJump = false;
                m_Slingshot.CanDragSlingshot = false;
            }
        }

        /// <summary>
        /// Set the initial position of the player.
        /// </summary>
        /// <param name="position">The position to set as initial position</param>
        private void SetInitialPosition(Vector3 position)
        {
            m_PlayerControllerLogic.InitialPosition = position;
        }

        /// <summary>
        /// Save the current spotlight radius
        /// </summary>
        private void SaveCurrentSpotlightRadius()
        {
            m_PlayerSpotlight.SaveCurrentSpotlightRadius();
        }

        private void SaveCurrentState()
        {
            SetInitialPosition(transform.position);
            SaveCurrentSpotlightRadius();
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
            m_PlayerControllerLogic.Jump(position);
        }
        
        private void OnLevelStarted()
        {
            IlluminateSpotlight(() =>
            {
                m_CanJump = true;
                EnableSlingshot();
            });
        }

        private void EnableSlingshot()
        {
            if (!m_CanJump)
            {
                return;
            }

            m_Slingshot.CanDragSlingshot = true;
        }

        private void DisableSlingshot()
        {
            m_Slingshot.CanDragSlingshot = false;
        }
    }
}

