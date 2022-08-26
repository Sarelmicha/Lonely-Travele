using System;
using System.Collections;
using System.Collections.Generic;
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
        
        private Vector2 m_InitialPosition;
        private float m_ConvertedSpeed;
        private bool m_IsMoving;

        private void Awake()
        {
            m_InitialPosition = transform.position;
            m_ConvertedSpeed = m_Speed * Time.deltaTime;
        }

        private void Update()
        {
            if (m_IsMoving)
            {
                Move();
            }
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, m_Target.position, m_ConvertedSpeed);

            if (IsReachedTarget())
            {
                OnTargetReached();
            }
        }

        private void OnTargetReached()
        {
            m_IsMoving = false;
            m_ShakeBehavior.TriggerShake();
        }

        private bool IsReachedTarget()
        {
            return Vector2.Distance(transform.position, m_Target.position) < m_ConvertedSpeed;
        }

        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            m_IsMoving = true;
        }
    }
  
}
