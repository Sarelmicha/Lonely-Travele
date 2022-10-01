using System.Collections.Generic;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Rewards.Point
{
    public class PointsStorage : MonoBehaviour
    {
        [SerializeField] private List<PointDisplay> m_PointsDisplays;
        private PointsStorageLogic m_PointsStorageLogic;
        private LevelManager m_LevelManager;

        private void Awake()
        {
            m_PointsStorageLogic = new PointsStorageLogic(m_PointsDisplays);
        }
        
        private void Start()
        {
            var go = GameObject.FindGameObjectWithTag("LevelManager");

            if (go == null)
            {
                Debug.Log("The level manager has not been found on scene.");
                return;
            }
      
            m_LevelManager = go.GetComponent<LevelManager>();
            m_LevelManager.OnLevelShouldRestart += Reset;
        }

        private void OnDestroy()
        {
            m_LevelManager.OnLevelShouldRestart -= Reset;
        }

        /// <summary>
        /// Called after point was collected by the player.
        /// Handle the Fill process for each point collected
        /// </summary>
        public void OnCollect()
        {
            m_PointsStorageLogic.OnCollect();
        }
        
        private void Reset(bool shouldFullReset)
        {
            m_PointsStorageLogic.Reset(shouldFullReset);
        }
    }
}