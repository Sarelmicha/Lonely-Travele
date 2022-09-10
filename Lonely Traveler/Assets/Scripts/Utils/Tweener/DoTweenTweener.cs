using System;
using DG.Tweening;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Utils
{
    public class DoTweenTweener : IMovementTweener
    {
        public void MoveTo(Transform obj, Vector3 targetPosition, float duration, MovementSwing movementSwing = null, Action onComplete = null)
        {
            var sequence = DOTween.Sequence();

            if (movementSwing != null)
            {
                sequence.Append(obj.DOMove(Vector3.Normalize(obj.position - targetPosition) * movementSwing.Burst, movementSwing.Duration));
            }

            sequence.Append(obj.DOMove(targetPosition, duration));
            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }
    }
}
