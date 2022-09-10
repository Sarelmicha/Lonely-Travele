using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HappyFlow.LonelyTraveler.Utils;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Utils
{
    public class DoTweenShake : IShakeTweener
    {
        public void Shake(Transform obj, float duration, float strength)
        {
            obj.DOShakePosition(duration, strength);
        }
    }
}

