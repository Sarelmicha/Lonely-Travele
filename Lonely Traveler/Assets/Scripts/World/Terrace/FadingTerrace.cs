using System.Collections;
using HappyFlow.LonelyTraveler.Player;
using UnityEngine;


namespace HappyFlow.LonelyTraveler.World.Terrace
{
    /// <summary>
    /// This class responsible for handling the Fading Terrace. whenever the player enter the collider of this terrace,
    /// The terrace will be fade after m_TimeToLiveInSeconds time.
    /// </summary>
    public class FadingTerrace : CollisionAbility
    {
        [SerializeField] private float m_TimeToLiveInSeconds;

        private float m_NumOfCurrentSeconds;
        private bool m_IsStay = false;
        
        private IEnumerator TryToFade()
        {
            while (m_IsStay && m_NumOfCurrentSeconds < m_TimeToLiveInSeconds)
            {
                m_NumOfCurrentSeconds++;
                yield return new WaitForSeconds(1);
            }
            
            if (m_IsStay)
            {
                //Do some call fading animation
                transform.gameObject.SetActive(false);
            }
        }

        protected override void OnPlayerCollisionEnter2D(PlayerController playerController)
        {
            m_IsStay = true;
            StartCoroutine(TryToFade());
        }

        protected override void OnPlayerCollisionExist2D(PlayerController playerController)
        {
            m_IsStay = false;
            m_NumOfCurrentSeconds = 0;
        }
    }
}