using UnityEngine;

namespace HappyFlow.LonelyTraveler.World.Enemies
{
    public class LightReducer : TriggerAbility
    {
        [SerializeField] private float m_LightToReduce;
        private bool m_IsReduce;
        
        private void Update()
        {
            if (m_IsPlayerInsideCollider)
            {
                ReduceLight();
            }
        }

        private void ReduceLight()
        {
            m_PlayerController.ReduceLight(m_LightToReduce);
        }
    }
}

