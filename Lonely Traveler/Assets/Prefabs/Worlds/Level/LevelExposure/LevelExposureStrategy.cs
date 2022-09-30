using System;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.LevelExposure
{
    /// <summary>
    /// Define the functionally of the <see cref="LevelExposureStrategy"/>
    /// </summary>
    public abstract class LevelExposureStrategy : MonoBehaviour
    {
        /// <summary>
        /// Expose the level to the user by a specific strategy 
        /// </summary>
        public abstract void Expose(Action onComplete = null);

        /// <summary>
        /// Reset the strategy
        /// </summary>
        /// <param name="shouldFullReset">A flag that indicate whether should we full reset the strategy</param>
        public abstract void Reset(bool shouldFullReset);
    }
}

