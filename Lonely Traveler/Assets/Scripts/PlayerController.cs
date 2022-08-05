using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D m_Rigidbody;
        [SerializeField] private TargetIndicator m_TargetIndicator;
        [SerializeField] private float m_Thrust;

        private PlayerControllerLogic m_PlayerControllerLogic;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_PlayerControllerLogic = new PlayerControllerLogic(m_TargetIndicator.TargetIndicatorLogic, m_Rigidbody, m_Thrust);
        }

        private void Start()
        {
            m_PlayerControllerLogic.SubsribeEvents();
        }

        private void OnDestroy()
        {
            m_PlayerControllerLogic.UnsubscribeEvents();
        }
    }
}

