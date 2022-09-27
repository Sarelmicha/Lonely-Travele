using System;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for handling all the logic of the slingshot
    /// </summary>
    public class SlingshotLogic
    {
        private readonly Transform m_SlingshotTransform;
        private readonly Transform m_SlingshotHolderTransform;
        private readonly float m_Radius;

        private Vector3 m_MousePosition;
        private bool m_IsDragging;
        
        /// <summary>
        /// Invoke when the player release the target on a certain location. 
        /// </summary>
        public event Action<Vector3> OnTargetReleased;

        public SlingshotLogic(Transform slingshotTransform, Transform slingshotHolderTransform, float radius)
        {
            m_SlingshotTransform = slingshotTransform;
            m_SlingshotHolderTransform = slingshotHolderTransform;
            m_Radius = radius;
        }

        public void TryUpdatePosition()
        {
            if (m_IsDragging)
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            m_MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Vector2.Distance(m_MousePosition, m_SlingshotHolderTransform.position) <= m_Radius)
            {
                SetPosition(m_MousePosition);
            }
            else
            {
                var direction = m_MousePosition - m_SlingshotHolderTransform.position;
                SetPosition(m_SlingshotHolderTransform.position + (Vector3.Normalize(direction) * m_Radius));
            }
        }

        private void SetPosition(Vector2 position)
        {
            m_SlingshotTransform.position = position;
        }

        /// <summary>
        /// Call when the mouse was released from the slingshot
        /// </summary>
        public void OnMouseUp()
        {
            m_IsDragging = false;
            OnTargetReleased?.Invoke(m_SlingshotHolderTransform.position - m_SlingshotTransform.position);
            SetPosition(m_SlingshotHolderTransform.position);
        }
        
        /// <summary>
        /// Call when the mouse was click on the the slingshot
        /// </summary>
        public void OnMouseDown()
        {
            m_IsDragging = true;
        }
    }
}