using System;
using System.Collections;
using DG.Tweening;
using HappyFlow.LonelyTraveler.Utils;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.LevelExposure
{
    public class TopDownStrategy : MonoBehaviour, ILevelExposureStrategy
    {
        [SerializeField] private Transform m_Target;
        [SerializeField] private float m_ToTargetDuration;
        [SerializeField] private float m_FromTargetDuration;
        [SerializeField] private float m_StartDelay;
        private Vector3 m_InitialPosition;
        private IMovementTweener m_MovementTweener;

        private void Awake()
        {
            m_InitialPosition = transform.position;
            m_MovementTweener = new DoTweenTweener();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(m_StartDelay);
            Expose();
        }

        public void Expose()
        {
            m_MovementTweener.MoveTo(transform, m_Target.position, m_ToTargetDuration, new MovementSwing(10, 1), () =>
            {
                m_MovementTweener.MoveTo(transform, m_InitialPosition, m_FromTargetDuration, new MovementSwing(10, 1));
            });
        }
    }
}
