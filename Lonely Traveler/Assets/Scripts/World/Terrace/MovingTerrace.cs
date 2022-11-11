using HappyFlow.LonelyTraveler.Player;
using HappyFlow.LonelyTraveler.Utils;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.Terrace
{
    public class MovingTerrace : TriggerAbility
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private float m_Speed;
        [SerializeField] private ShakeBehavior m_ShakeBehavior;
        private IMovementTweener m_MovementTweener;
        private Vector3 m_InitialPosition;
        private Transform m_PreviousPlayerParent;

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
            m_MovementTweener.MoveTo(transform, m_Target.position, m_Speed, null, OnTargetReached);
        }

        private void OnTargetReached()
        {
            m_ShakeBehavior.Shake();
        }

        private bool IsReachedTarget()
        {
            return Vector2.Distance(transform.position, m_Target.position) < m_Speed * Time.deltaTime;
        }

        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            if (IsReachedTarget())
            {
                return;
            }

            m_PreviousPlayerParent = playerController.transform.parent;
            playerController.transform.SetParent(transform);
            Move();
        }

        protected override void OnPlayerTriggerExit2D(PlayerController playerController)
        {
            playerController.transform.SetParent(m_PreviousPlayerParent);
        }

        protected override void Reset(bool shouldFullReset)
        {
            m_MovementTweener.StopTween();
            m_IsMoving = false;
            transform.position = m_InitialPosition;
        }
    }
}
