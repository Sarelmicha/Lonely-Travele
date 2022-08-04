using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private TargetIndicator m_TargetIndicator;
        [SerializeField] private float m_Thrust;

        private Rigidbody2D m_Rigidbody;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            SubsribeEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubsribeEvents()
        {
            m_TargetIndicator.OnTargetReleased += Shoot;
        }

        private void UnsubscribeEvents()
        {
            m_TargetIndicator.OnTargetReleased -= Shoot;
        }

        private void Shoot(Vector3 vector)
        {
            Debug.Log("shoot!" + vector);
            m_Rigidbody.AddForce(vector * m_Thrust);
        }
    }
}

