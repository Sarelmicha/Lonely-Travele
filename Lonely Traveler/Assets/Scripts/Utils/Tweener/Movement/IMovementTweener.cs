using System;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Utils
{
    public interface IMovementTweener
    {
        void MoveTo(Transform obj, Vector3 targetPosition, float duration, MovementSwing movementSwing = null,
            Action onComplete = null);
    }
}