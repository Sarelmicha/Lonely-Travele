using UnityEngine;
using UnityEngine.Serialization;

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
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_PlayerControllerLogic = new PlayerControllerLogic(m_Slingshot.SlingshotLogic, m_Rigidbody, m_Thrust);
        }

        private void Start()
        {
            m_PlayerControllerLogic.SubscribeEvents();
        }

        private void OnDestroy()
        {
            m_PlayerControllerLogic.UnsubscribeEvents();
        }
    }
}

