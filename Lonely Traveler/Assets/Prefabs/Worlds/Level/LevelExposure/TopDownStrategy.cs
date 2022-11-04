using System;
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
        private Vector3 m_InitialPosition;
        private IMovementTweener m_MovementTweener;
        private LevelManager m_LevelManager;
        
        private void Awake()
        {
            m_InitialPosition = transform.position;
            m_MovementTweener = new DoTweenTweener();
        }
        
        /// <summary>
        /// Expose the level to the user by a specific strategy 
        /// </summary>
        public override void Expose(Action onComplete = null)
        {
            m_MovementTweener.MoveTo(transform, m_Target.position, m_ToTargetDuration, new MovementSwing(10, 1), () =>
            {
                m_MovementTweener.MoveTo(transform, m_InitialPosition, m_FromTargetDuration, null, () =>
                {
                    onComplete?.Invoke();
                });
            });
        }

        /// <summary>
        /// Reset the strategy
        /// </summary>
        /// <param name="shouldFullReset">A flag that indicate whether should we full reset the strategy</param>
        public override void Reset(bool shouldFullReset)
        {
            m_MovementTweener.StopTween();
            transform.position = m_InitialPosition;
        }
    }
}
