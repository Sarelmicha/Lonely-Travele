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
        private SlingshotLogic SlingshotLogic => m_SlingshotLogic ??= new SlingshotLogic(transform, SlingshotHolder.transform, SlingshotHolder.radius);

        private SlingshotLogic m_SlingshotLogic;

        private void Awake()
        {
            SlingshotHolder.enabled = false;
        }

        private void Update()
        {          
            SlingshotLogic.TryUpdatePosition();         
        }
     
        private void OnMouseDown()
        {
            if (!CanDragSlingshot)
            {
                return;
            }

            Appear();
            SlingshotLogic.OnMouseDown();
        }

        private void OnMouseUp()
        {
            Disappear();
            SlingshotLogic.OnMouseUp();
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
            SlingshotLogic.OnTargetReleased += action;
        }
        
        /// <summary>
        /// Unsubscribe to the OnTargetReleased event
        /// </summary>
        /// <param name="action">The action to remove from invoking when the OnTargetReleased event has invoke</param>
        public void UnsubscribeOnTargetReleasedEvent(Action<Vector3> action)
        {
            SlingshotLogic.OnTargetReleased -= action;
        }
    }
}