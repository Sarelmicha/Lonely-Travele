namespace HappyFlow.LonelyTraveler.Rewards
{
    /// <summary>
    /// Define the functionality of a generic reward
    /// </summary>
    public interface IReward
    {
        /// <summary>
        /// Called when the reward was collected by the player
        /// </summary>
        void Collect();
    }
}

