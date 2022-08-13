using HappyFlow.LonelyTraveler.Player;

namespace HappyFlow.LonelyTraveler.World
{
    public class CheckPoint : SpecialAbility
    {
        protected override void InvokeAbility(PlayerController playerController)
        {
            playerController.SetInitialPosition(transform.position);
        }
    }
}