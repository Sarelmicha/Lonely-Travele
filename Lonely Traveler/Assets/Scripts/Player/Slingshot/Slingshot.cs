using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for control the logic and UI of the Slingshot
    /// </summary>
    public class Slingshot : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D SlingshotHolder;
        [SerializeField] private Image m_Image;

        public bool CanDragSlingshot { get; set; }

        /// <summary>
        /// Responsible for handling all the logic of the slingshot
        /// </summary>
        private SlingshotLogic m_SlingshotLogic;

        private void Awake()
        {
            m_SlingshotLogic = new SlingshotLogic(transform, SlingshotHolder.transform, SlingshotHolder.radius);
            SlingshotHolder.enabled = false;
        }

        private void Update()
        {          
            m_SlingshotLogic.TryUpdatePosition();         
        }
     
        private void OnMouseDown()
        {
            if (!CanDragSlingshot)
            {
                return;
            }

            Appear();
            m_SlingshotLogic.OnMouseDown();
        }

        private void OnMouseUp()
        {
            Disappear();
            m_SlingshotLogic.OnMouseUp();
        }

        private void Appear()
        {
            m_Image.enabled = true;
        }

        private void Disappear()
        {
            m_Image.enabled = false;
        }

        /// <summary>
        /// Subscribe to the OnTargetReleased event
        /// </summary>
        /// <param name="action">The action to invoke when the OnTargetReleased event has invoke</param>
        public void SubscribeOnTargetReleasedEvent(Action<Vector3> action)
        {
            m_SlingshotLogic.OnTargetReleased += action;
        }
        
        /// <summary>
        /// Unsubscribe to the OnTargetReleased event
        /// </summary>
        /// <param name="action">The action to remove from invoking when the OnTargetReleased event has invoke</param>
        public void UnsubscribeOnTargetReleasedEvent(Action<Vector3> action)
        {
            m_SlingshotLogic.OnTargetReleased -= action;
        }
    }
}