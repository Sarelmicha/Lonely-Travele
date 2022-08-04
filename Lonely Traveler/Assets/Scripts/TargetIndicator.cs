using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    public class TargetIndicator : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D m_Base;
        private bool m_IsDragging;
        private Vector2 m_MousePosition;

        /// <summary>
        /// Invoke when the player relase the target on a certin location. 
        /// </summary>
        public event Action<Vector3> OnTargetReleased;

        private void Update()
        {
            if (m_IsDragging)
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            m_MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(m_MousePosition, m_Base.transform.position) <= m_Base.radius)
            {
                SetPosition(m_MousePosition);
            }
            else
            {
                SetPosition(Vector3.Normalize(m_MousePosition) * m_Base.radius);
            }
        }

        private void OnMouseDown()
        {
            m_IsDragging = true;
        }

        private void OnMouseUp()
        {
            m_IsDragging = false;
            OnTargetReleased?.Invoke(-(Vector3.Normalize(transform.position) * Vector2.Distance(transform.position, m_Base.transform.position)));
            SetPosition(m_Base.transform.position);
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}