using System;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World
{
    /// <summary>
    /// Responsible for the UI of the <see cref="LevelChecker"/>
    /// </summary>
    public class LevelCheckerDisplay : MonoBehaviour
    {
        [SerializeField] private Animator m_CheckerAnimation;
        [SerializeField] private LevelChecker m_LevelChecker;

        private const string ENTER_CHECKER_ANIMATION = "Enter Checker";
        private const string LEAVE_CHECKER_ANIMATION = "Leave Checker";

        private void OnEnable()
        {
            m_LevelChecker.OnPlayerEnterLevelChecker += Show;
            m_LevelChecker.OnPlayerExitLevelChecker += Hide;
        }

        private void OnDisable()
        {
            m_LevelChecker.OnPlayerEnterLevelChecker -= Show;
            m_LevelChecker.OnPlayerExitLevelChecker -= Hide;
        }

        /// <summary>
        /// Show an indication to the user that the level player enter the checker
        /// </summary>
        private void Show()
        {
            m_CheckerAnimation.Play(ENTER_CHECKER_ANIMATION);
        }

        /// <summary>
        /// Show an indication to the user that the level player left the checker
        /// </summary>
        private void Hide()
        {
            m_CheckerAnimation.Play(LEAVE_CHECKER_ANIMATION);
        }
    }
}
