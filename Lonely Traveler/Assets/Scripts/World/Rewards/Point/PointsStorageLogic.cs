using System.Collections.Generic;

namespace HappyFlow.LonelyTraveler.World.Rewards
{
    public class PointsStorageLogic
    {
        private int m_InitialPointsAmountCollected;
        private readonly List<PointDisplay> m_PointsDisplays;
        private readonly List<Point> m_PointsCollected;

        public PointsStorageLogic(List<PointDisplay> pointsDisplays)
        {
            m_PointsCollected = new List<Point>();
            m_PointsDisplays = pointsDisplays;
            m_InitialPointsAmountCollected = 0;
        }
        /// <summary>
        /// Called after point was collected by the player.
        /// Handle the Fill process for each point collected
        /// </summary>
        public void OnCollect(Point point)
        {
            m_PointsCollected.Add(point);
            
            if (m_PointsCollected.Count > m_PointsDisplays.Count || m_PointsDisplays[m_PointsCollected.Count - 1].IsFull)
            {
                return;
            }

            m_PointsDisplays[m_PointsCollected.Count - 1].Fill();
        }

        /// <summary>
        /// Reset the points storage.
        /// </summary>
        /// <param name="shouldFullReset">A flag that indicate whether we should full reset</param>
        public void Reset(bool shouldFullReset)
        {
           if(shouldFullReset)
           {
               foreach (var displayPoint in m_PointsDisplays)
               {
                   displayPoint.Reset();
               }

               foreach (var point in m_PointsCollected)
               {
                   point.Reset();
               }
               
               m_PointsCollected.Clear();
           }
           else
           {
               for (var i = m_PointsDisplays.Count - 1; i >= m_InitialPointsAmountCollected; i--)
               {
                   m_PointsDisplays[i].Reset();
               }
               
               for (var i = m_PointsCollected.Count - 1; i >= m_InitialPointsAmountCollected; i--)
               {
                   m_PointsCollected[i].Reset();
                   m_PointsCollected.RemoveAt(i);
               }
           }
        }

        /// <summary>
        /// Save the initial points amount collected as the current amount collected.
        /// </summary>
        public void SaveCurrentPointsAmount()
        {
            m_InitialPointsAmountCollected = m_PointsCollected.Count;
        }
    }
}