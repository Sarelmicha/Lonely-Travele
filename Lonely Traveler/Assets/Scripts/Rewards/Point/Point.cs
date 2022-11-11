using HappyFlow.LonelyTraveler.Player;
using HappyFlow.LonelyTraveler.World;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Rewards.Point
{
    /// <summary>
    /// This class responsible for handling the point reward (including handling UI and logic).
    /// </summary>
    public class Point : TriggerAbility, IReward
    {
        [SerializeField] private PointsStorage m_PointsStorage;

        /// <summary>
        /// Show on UI and Notify Point storage that the point was collected.
        /// </summary>
        public void Collect()
        {
            // Do some animation to show star was collected.
            m_PointsStorage.OnCollect(this);
            gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Reset the point to its initial state.
        /// </summary>
        public void Reset()
        {
            gameObject.SetActive(true);
        }

        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            Collect();
        }
    }
}