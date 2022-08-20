using HappyFlow.LonelyTraveler.Player;

namespace HappyFlow.LonelyTraveler.World
{
    /// <summary>
    /// This class responsible for handling the Spike enemy. whenever the player hit the spike, player will die.
    /// </summary>
    public class Spike : TriggerAbility
    {
        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            playerController.Die();
        }
    } 
}

