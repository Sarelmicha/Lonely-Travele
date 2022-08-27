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

        public void Collect()
        {
            // Do some animation to show star was collected.
            m_PointsStorage.OnCollect();
        }

        protected override void OnPlayerTriggerEnter2D(PlayerController playerController)
        {
            Collect();
        }
    }
}