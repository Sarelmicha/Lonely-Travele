using System;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for handling the collision of the player.
    /// </summary>
    public class PlayerCollider : MonoBehaviour
    {
        /// <summary>
        /// Invoke when the player is grounded.
        /// </summary>
        public event Action OnPlayerGrounded;
        
        /// <summary>
        /// Invoke when the player is ungrounded.
        /// </summary>
        public event Action OnPlayerUngrounded;

        private const string GROUND = "Ground";

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(GROUND))
            {
                OnPlayerGrounded?.Invoke();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GROUND))
            {
                OnPlayerUngrounded?.Invoke();
            }
        }
    }
}