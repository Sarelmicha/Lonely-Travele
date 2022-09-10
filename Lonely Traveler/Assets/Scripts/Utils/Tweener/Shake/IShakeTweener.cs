using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HappyFlow.LonelyTraveler.Utils
{
    public interface IShakeTweener
    {
        void Shake(Transform obj, float duration, float strength);
    }  
}

