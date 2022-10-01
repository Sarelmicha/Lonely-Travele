using System.Collections.Generic;

namespace HappyFlow.LonelyTraveler.Rewards.Point
{
    public class PointsStorageLogic
    {
        private int m_CurrentPoint;
        private List<PointDisplay> m_PointsDisplays;

        public PointsStorageLogic(List<PointDisplay> pointsDisplays)
        {
            m_PointsDisplays = pointsDisplays;
            m_CurrentPoint = 0;
        }
        /// <summary>
        /// Called after point was collected by the player.
        /// Handle the Fill process for each point collected
        /// </summary>
        public void OnCollect()
        {
            if (m_CurrentPoint < 0 || m_CurrentPoint > m_PointsDisplays.Count - 1 ||
                m_PointsDisplays[m_CurrentPoint].IsFull)
            {
                return;
            }

            m_PointsDisplays[m_CurrentPoint].Fill();
            m_CurrentPoint++;
        }

        /// <summary>
        /// Reset the points storage.
        /// </summary>
        /// <param name="shouldFullReset">A flag that indicate whether we should full reset</param>
        public void Reset(bool shouldFullReset)
        {
            foreach (var displayPoint in m_PointsDisplays)
            {
                displayPoint.Reset(shouldFullReset);
            }
            
            m_CurrentPoint = 0;
        }
    }
}