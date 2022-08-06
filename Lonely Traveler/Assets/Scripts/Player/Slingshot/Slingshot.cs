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
      
        /// <summary>
        /// Responsible for handling all the logic of the slingshot
        /// </summary>
        public SlingshotLogic SlingshotLogic { get; private set; }

        private void Awake()
        {
            SlingshotLogic = new SlingshotLogic(transform, SlingshotHolder.transform, SlingshotHolder.radius);
        }

        private void Update()
        {          
            SlingshotLogic.TryUpdatePosition();         
        }
     
        private void OnMouseDown()
        {
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
    }
}