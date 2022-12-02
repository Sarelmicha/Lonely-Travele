using HappyFlow.LonelyTraveler.Core;
using HappyFlow.LonelyTraveler.UI;
using HappyFlow.LonelyTraveler.Utils;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.UI
{
    public class RestartButton : MonoBehaviour, IButton
    {
        /// <summary>
        /// The command that should be executed when the button is Restart Button is clicked.
        /// </summary>
        public ICommand Command { get; set; }
    
        private void Awake()
        {
            var go = GameObject.FindGameObjectWithTag("LevelManager");

            if (go == null)
            {
                Debug.Log("The level manager has not been found on scene.");
                return;
            }

            var levelManager = go.GetComponent<LevelManager>();
            Command = new RestartCommand(levelManager);
        }

        /// <summary>
        /// Call the <see cref="RestartCommand"/> to restart the game.
        /// </summary>
        public void OnClick()
        {
            Command.Execute();
        }
    }

}
