using HappyFlow.LonelyTraveler.Player;
using HappyFlow.LonelyTraveler.World;
using UnityEngine;

public class ForceField : TriggerAbility
{
    [SerializeField] private Vector2 m_Force;
    protected override void OnPlayerTriggerStay2D(PlayerController playerController)
    {
        playerController.AddForce(m_Force);
    }
}
