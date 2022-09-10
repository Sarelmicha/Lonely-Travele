using HappyFlow.LonelyTraveler.Player;
using HappyFlow.LonelyTraveler.Utils;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World
{
    public class FollowCamera : TriggerAbility
    {
        [SerializeField] private bool m_CanFollow;
        private IMovementTweener m_MovementTweener;

        private void Awake()
        {
            m_MovementTweener = new DoTweenTweener();
        }

        protected override void OnPlayerTriggerExit2D(PlayerController playerController)
        {
            if (!m_CanFollow)
            {
                return;
            }

            m_MovementTweener.MoveTo(transform, 
                new Vector3(transform.position.x, playerController.transform.position.y - transform.position.y , transform.position.z)
                , 1f);
        }
    }
}