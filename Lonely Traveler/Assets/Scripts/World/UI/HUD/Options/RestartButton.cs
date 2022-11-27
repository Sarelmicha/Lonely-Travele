using HappyFlow.LonelyTraveler.Core;
using HappyFlow.LonelyTraveler.UI;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.UI
{
    public class RestartButton : MonoBehaviour, IButton
    {
        private LevelManager m_LevelManager;
    
        private void Awake()
        {
            var go = GameObject.FindGameObjectWithTag("LevelManager");

            if (go == null)
            {
                Debug.Log("The level manager has not been found on scene.");
                return;
            }
      
            m_LevelManager = go.GetComponent<LevelManager>();
        }

        public void OnClick()
        {
            m_LevelManager.StartRestartLevel(true);
        }
    }

}
