using HappyFlow.LonelyTraveler.Player;
using HappyFlow.LonelyTraveler.World;
using UnityEngine;

namespace HappyFlow.LonelyTraveler
{
    public class Boundary : TriggerAbility
    {
        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            playerController.Die();
        }
    }
}