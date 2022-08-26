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

        private IEnumerator TryToFade()
        {
            while (m_IsPlayerInsideCollider && m_NumOfCurrentSeconds < m_TimeToLiveInSeconds)
            {
                m_NumOfCurrentSeconds++;
                yield return new WaitForSeconds(1);
            }
            
            if (m_IsPlayerInsideCollider)
            {
                //Do some call fading animation
                transform.gameObject.SetActive(false);
            }
        }

        protected override void OnPlayerCollisionEnter2D(PlayerController playerController)
        {
            StartCoroutine(TryToFade());
        }

        protected override void OnPlayerCollisionExist2D(PlayerController playerController)
        {
            m_NumOfCurrentSeconds = 0;
        }
    }
}