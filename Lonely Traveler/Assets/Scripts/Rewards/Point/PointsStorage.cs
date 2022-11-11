using System.Collections.Generic;
using HappyFlow.LonelyTraveler.World;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Rewards.Point
{
    public class PointsStorage : MonoBehaviour
    {
        [SerializeField] private List<PointDisplay> m_PointsDisplays;
        private PointsStorageLogic m_PointsStorageLogic;
        private LevelManager m_LevelManager;

        /// <summary>
        /// Called after point was collected by the player.
        /// Handle the Fill process for each point collected
        /// </summary>
        /// <param name="point"></param>
        public void OnCollect(Point point)
        {
            m_PointsStorageLogic.OnCollect(point);
        }
        
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
            m_LevelManager.OnStateShouldBeSaved += SaveCurrentPointsAmount;
        }

        private void OnDestroy()
        {
            m_LevelManager.OnLevelShouldRestart -= Reset;
            m_LevelManager.OnStateShouldBeSaved -= SaveCurrentPointsAmount;
        }
        
        private void Reset(bool shouldFullReset)
        {
            m_PointsStorageLogic.Reset(shouldFullReset);
        }

        /// <summary>
        /// Save the initial points amount collected as the current amount collected.
        /// </summary>
        private void SaveCurrentPointsAmount()
        {
            m_PointsStorageLogic.SaveCurrentPointsAmount();
        }
    }
}