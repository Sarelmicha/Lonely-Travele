using HappyFlow.LonelyTraveler.Core;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.UI
{
    /// <summary>
    /// Implement the <see cref="ICommand"/> pattern.
    /// Use to restart the game.
    /// </summary>
    public class RestartCommand : ICommand
    {
        private readonly LevelManager m_LevelManager;
        
        public RestartCommand(LevelManager levelManager)
        {
            if (levelManager == null)
            {
                Debug.Log($"Cannot initialize {nameof(RestartCommand)} when levelManager is null");
                return;
            }

            m_LevelManager = levelManager;
        }

        /// <summary>
        /// Restart the level
        /// </summary>
        public void Execute()
        {
            m_LevelManager.StartRestartLevel(true);
        }
    }
}