using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HappyFlow.LonelyTraveler.Player
{
    public class TargetIndicator : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D m_Base;
        [SerializeField] private Image m_Image;
      
        public TargetIndicatorLogic TargetIndicatorLogic { get; private set; }

        private void Awake()
        {
            TargetIndicatorLogic = new TargetIndicatorLogic(transform, m_Base.transform, m_Base.radius);
        }

        private void Update()
        {          
            TargetIndicatorLogic.TryUpdatePosition();         
        }
     
        private void OnMouseDown()
        {
            Appear();
            TargetIndicatorLogic.OnMouseDown();
        }

        private void OnMouseUp()
        {
            Disappear();
            TargetIndicatorLogic.OnMouseUp();
        }

        private void Appear()
        {
            m_Image.enabled = true;
        }

        private void Disappear()
        {
            m_Image.enabled = false;
        }
    }
}