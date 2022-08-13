using HappyFlow.LonelyTraveler.Player;

namespace HappyFlow.LonelyTraveler.World
{
    /// <summary>
    /// This class responsible for handling the Spike enemy. whenever the player hit the spike, player will die.
    /// </summary>
    public class Spike : SpecialAbility
    {
        protected override void InvokeAbility(PlayerController playerController)
        {
            playerController.Die();
        }
    } 
}

