using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

namespace HappyFlow.LonelyTraveler
{
    public class Boundary : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var player = col.GetComponent<PlayerController>();
            
            if (!player)
            {
                return;
            }

            player.Reset();
        }
    }
}