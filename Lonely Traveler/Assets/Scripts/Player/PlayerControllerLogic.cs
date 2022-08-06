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

        /// <summary>
        /// Constructor for <see cref="PlayerControllerLogic"/>
        /// </summary>
        /// <param name="slingshotLogic">An instance of the <see cref="SlingshotLogic"/></param>
        /// <param name="playerRigidbody">The player rigidbody</param>
        /// <param name="thrust">A float that will indicate the thrust of the player jump</param>
        public PlayerControllerLogic(SlingshotLogic slingshotLogic, Rigidbody2D playerRigidbody, float thrust)
        {
            m_SlingshotLogic = slingshotLogic;
            m_PlayerRigidbody = playerRigidbody;
            m_Thrust = thrust;
        }

        /// <summary>
        /// Subscribe the player to events
        /// </summary>
        public void SubscribeEvents()
        {
            m_SlingshotLogic.OnTargetReleased += Jump;
        }

        /// <summary>
        /// Unsubscribe the player to events
        /// </summary>
        public void UnsubscribeEvents()
        {
            m_SlingshotLogic.OnTargetReleased -= Jump;
        }

        private void Jump(Vector3 vector)
        {
            m_PlayerRigidbody.AddForce(vector * m_Thrust);
        }
    }
}