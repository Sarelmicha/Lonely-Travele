using HappyFlow.LonelyTraveler.Player;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World
{
    /// <summary>
    /// This class responsible for finish the level
    /// </summary>
    public class LevelChecker : TriggerAbility
    {
        [SerializeField] private float m_ClickDuration = 2;
        [SerializeField] private LevelCheckerDisplay m_LevelCheckerDisplay;

        private bool m_IsClicking = false;
        private float m_TotalDownTime = 0;

        private void Update()
        {
            if (!m_IsPlayerInsideCollider)
            {
                return;
            }

            // Detect the first click
            if (Input.GetMouseButtonDown(0))
            {
                m_TotalDownTime = 0;
                m_IsClicking = true;
            }

            // If a first click detected, and still clicking,
            // measure the total click time, and fire an event
            // if we exceed the duration specified
            if (m_IsClicking && Input.GetMouseButton(0))
            {
                m_TotalDownTime += Time.deltaTime;

                if (m_TotalDownTime >= m_ClickDuration)
                {
                   m_LevelManager.FinishLevel();
                    m_IsClicking = false;
                }
            }

            // If a first click detected, and we release before the
            // duration, do nothing, just cancel the click
            if (m_IsClicking && Input.GetMouseButtonUp(0))
            {
                m_IsClicking = false;
            }
        }

        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            m_LevelCheckerDisplay.Show();
        }

        protected override void OnPlayerTriggerExit2D(PlayerController playerController)
        {
            m_LevelCheckerDisplay.Hide();
        }
    } 
}

