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
            m_PlayerSpotlight = playerSpotlight;
        }
        
        /// <summary>
        /// Make the player jump in a certain direction and a <see cref="m_Thrust"/> force.
        /// </summary>
        /// <param name="vector">The direction to jump</param>
        public void Jump(Vector3 vector)
        {
            m_PlayerRigidbody.AddForce(vector * m_Thrust);
        }
        
        /// <summary>
        /// Reset the player position in the initial position and set it velocity to zero
        /// </summary>
        public void Reset()
        {
            m_PlayerRigidbody.transform.position = InitialPosition;
            m_PlayerRigidbody.velocity = Vector2.zero;
            m_PlayerSpotlight.Reset();
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