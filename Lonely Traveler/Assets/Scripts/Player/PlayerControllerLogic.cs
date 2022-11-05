using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for handling all the logic of the <see cref="PlayerController"/>
    /// </summary>
    public class PlayerControllerLogic
    {
        private readonly Rigidbody2D m_PlayerRigidbody;
        private readonly float m_Thrust;
        private readonly PlayerSpotlight m_PlayerSpotlight;
        public Vector3 InitialPosition { get; set; }
        private readonly Vector3 m_StartLevelPosition;

        /// <summary>
        /// Constructor for <see cref="PlayerControllerLogic"/>
        /// </summary>
        /// <param name="playerRigidbody">The player rigidbody</param>
        /// <param name="thrust">A float that will indicate the thrust of the player jump</param>
        /// <param name="initialPosition">The initial position of the player</param>
        public PlayerControllerLogic(Rigidbody2D playerRigidbody, float thrust, Vector3 initialPosition, PlayerSpotlight playerSpotlight)
        {
            m_PlayerRigidbody = playerRigidbody;
            m_Thrust = thrust;
            InitialPosition = initialPosition;
            m_StartLevelPosition = initialPosition;
            m_PlayerSpotlight = playerSpotlight;
        }
        
        /// <summary>
        /// Make the player jump in a certain direction and a <see cref="m_Thrust"/> force.
        /// </summary>
        /// <param name="direction">The direction to jump</param>
        public void Jump(Vector2 direction)
        {
            m_PlayerRigidbody.velocity = direction * m_Thrust;
        }

        /// <summary>
        /// Reset the player position in the initial position and set it velocity to zero
        /// </summary>
        /// <param name="shouldFullReset">A flag that indicate whether to full reset the player controller or not.</param>
        public void Reset(bool shouldFullReset)
        {
            if (shouldFullReset)
            {
                InitialPosition = m_StartLevelPosition;
            }
            
            m_PlayerRigidbody.velocity = Vector2.zero;
            m_PlayerRigidbody.transform.position = InitialPosition;
            m_PlayerSpotlight.Reset(shouldFullReset);
        }

        /// <summary>
        /// Apply force to the player in a certain vector
        /// </summary>
        /// <param name="force"></param>
        public void AddForce(Vector2 force)
        {
            m_PlayerRigidbody.AddForce(force);
        }
    }
}