using HappyFlow.LonelyTraveler.Player;
using HappyFlow.LonelyTraveler.Utils;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.Terrace
{
    /// <summary>
    /// This class responsible for moving a terrace to a specific target on a certain speed.
    /// </summary>
    public class MovingTerrace : TriggerAbility
    {
        [SerializeField] private Transform m_Destination;
        [SerializeField] private float m_Duration;
        [SerializeField] private ShakeBehavior m_ShakeBehavior;
        private IMovementTweener m_MovementTweener;
        private Vector3 m_InitialPosition;

        private bool m_IsMoving;

        private void Awake()
        {
            m_MovementTweener = new DoTweenTweener();
            m_InitialPosition = transform.position;
        }
        
        private void Move()
        {
            if (m_IsMoving)
            {
                return;
            }

            m_IsMoving = true;
            m_MovementTweener.MoveTo(transform, m_Destination.position, m_Duration, null, OnReachedDestination);
        }

        private void OnReachedDestination()
        {
            m_ShakeBehavior?.Shake();
        }

        private bool IsReachedTarget()
        {
            return Vector2.Distance(transform.position, m_Destination.position) < m_Duration * Time.deltaTime;
        }

        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            if (IsReachedTarget())
            {
                return;
            }

            playerController.transform.SetParent(transform);
            Move();
        }

        protected override void OnPlayerTriggerExit2D(PlayerController playerController)
        {
            playerController.transform.SetParent(playerController.OriginalParent);
        }

        protected override void Reset(bool shouldFullReset)
        {
            m_MovementTweener.StopTween();
            m_IsMoving = false;
            transform.position = m_InitialPosition;
        }
    }
}
