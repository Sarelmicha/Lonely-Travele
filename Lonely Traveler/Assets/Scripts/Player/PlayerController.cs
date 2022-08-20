using UnityEngine;

namespace HappyFlow.LonelyTraveler.Player
{
    /// <summary>
    /// This class responsible for control the logic and UI of the player.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D m_Rigidbody;
        [SerializeField] private Slingshot m_Slingshot;
        [SerializeField] private float m_Thrust;

        private PlayerControllerLogic m_PlayerControllerLogic;

        private void Awake()
        {
            m_PlayerControllerLogic = new PlayerControllerLogic(m_Rigidbody, m_Thrust, transform.position);
        }

        private void Start()
        {
            m_Slingshot.SubscribeOnTargetReleasedEvent(m_PlayerControllerLogic.Jump);
        }

        private void OnDestroy()
        {
            m_Slingshot.UnsubscribeOnTargetReleasedEvent(m_PlayerControllerLogic.Jump);
        }
        
        public void Die()
        {
            //Do some die animation
            m_PlayerControllerLogic.Reset();
        }

        public void Reset()
        {
            m_PlayerControllerLogic.Reset();
            //Do some awake animation
        }

        public void SetInitialPosition(Vector3 position)
        {
            m_PlayerControllerLogic.InitialPosition = position;
        }
    }
}

