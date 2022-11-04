using System;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for handling the collision of the player.
    /// </summary>
    public class PlayerCollider : MonoBehaviour
    {
        public event Action OnPlayerGrounded;
        public event Action OnPlayerUngrounded;

        private const string GROUND = "Ground";

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(GROUND))
            {
                Debug.Log("Player grounded.");
                OnPlayerGrounded?.Invoke();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GROUND))
            {
                Debug.Log("Player not grounded.");
                OnPlayerUngrounded?.Invoke();
            }
        }
    }
}