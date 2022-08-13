using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World
{
    /// <summary>
    /// This class responsible for handling the enemy. This is an abstract class and can be inherit by new enemies using the Damage method.
    /// </summary>
    public abstract class Enemy : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var player = col.GetComponent<PlayerController>();

            if (!player)
            {
                return;
            }
      
            Damage(player);
        }
    
        protected abstract void Damage(PlayerController playerController);
    }
}

