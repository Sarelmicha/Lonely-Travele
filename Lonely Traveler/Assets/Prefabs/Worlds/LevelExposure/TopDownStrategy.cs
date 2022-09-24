using System;
using System.Collections;
using HappyFlow.LonelyTraveler.Utils;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.LevelExposure
{
    /// <summary>
    /// This class responsible for exposing the level to the user with a Top Down Strategy.
    /// The camera will move from bottom to top to the target and from the target to the bottom to the initial point in order
    /// to expose the level to the user.
    /// </summary>
    public class TopDownStrategy : LevelExposureStrategy
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private float m_ToTargetDuration;
        [SerializeField] private float m_FromTargetDuration;
        [SerializeField] private float m_StartDelay;
        private Vector3 m_InitialPosition;
        private IMovementTweener m_MovementTweener;
        private Collider2D m_Collider2D;

        private void Awake()
        {
            m_InitialPosition = transform.position;
            m_MovementTweener = new DoTweenTweener();
            m_Collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            // enable collider before starting expose the top done strategy
            m_Collider2D.enabled = false;
        }

        /// <summary>
        /// Expose the level to the user by a specific strategy 
        /// </summary>
        public override void Expose(Action onComplete = null)
        {
            m_MovementTweener.MoveTo(transform, m_Target.position, m_ToTargetDuration, new MovementSwing(10, 1), () =>
            {
                m_MovementTweener.MoveTo(transform, m_InitialPosition, m_FromTargetDuration, new MovementSwing(10, 1), () =>
                {
                    m_Collider2D.enabled = true;
                    onComplete?.Invoke();
                });
            });
        }
    }
}
