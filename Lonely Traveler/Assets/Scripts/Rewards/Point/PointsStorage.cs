using System.Collections.Generic;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Rewards.Point
{
    public class PointsStorage : MonoBehaviour
    {
        [SerializeField] private List<PointDisplay> m_PointsDisplays;
        private PointsStorageLogic m_PointsStorageLogic;
        private void Awake()
        {
            m_PointsStorageLogic = new PointsStorageLogic(m_PointsDisplays);
        }
        
        /// <summary>
        /// Called after point was collected by the player.
        /// Handle the Fill process for each point collected
        /// </summary>
        public void OnCollect()
        {
            m_PointsStorageLogic.OnCollect();
        }
    }
}