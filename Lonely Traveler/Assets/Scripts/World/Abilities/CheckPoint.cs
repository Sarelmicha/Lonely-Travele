using HappyFlow.LonelyTraveler.Player;

namespace HappyFlow.LonelyTraveler.World
{
    /// <summary>
    /// This class responsible for handling the CheckPoint. whenever the player reach the check point, the initial position of the player
    /// will set to the check point position.
    /// </summary>
    public class CheckPoint : TriggerAbility
    {
        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            playerController.SetInitialPosition(transform.position);
            playerController.SaveCurrentSpotlightRadius();
        }
    }
}