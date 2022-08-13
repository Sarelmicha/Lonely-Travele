using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for handling all the logic of the <see cref="PlayerController"/>
    /// </summary>
    public class PlayerControllerLogic
    {
        private readonly SlingshotLogic m_SlingshotLogic;
        private readonly Rigidbody2D m_PlayerRigidbody;
        private readonly float m_Thrust;
        public Vector3 InitialPosition { get; set; }

        /// <summary>
        /// Constructor for <see cref="PlayerControllerLogic"/>
        /// </summary>
        /// <param name="playerRigidbody">The player rigidbody</param>
        /// <param name="thrust">A float that will indicate the thrust of the player jump</param>
        public PlayerControllerLogic(Rigidbody2D playerRigidbody, float thrust, Vector3 initialPosition)
        {
            m_PlayerRigidbody = playerRigidbody;
            m_Thrust = thrust;
            InitialPosition = initialPosition;
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
        }
    }
}