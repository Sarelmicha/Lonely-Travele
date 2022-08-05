using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLogic
{
    private TargetIndicatorLogic m_TargetIndicatorLogic;
    private Rigidbody2D m_Rigidbody;
    private float m_Thrust;
    public PlayerControllerLogic(TargetIndicatorLogic targetIndicatorLogic, Rigidbody2D rigidbody, float thrust)
    {
        m_TargetIndicatorLogic = targetIndicatorLogic;
        m_Rigidbody = rigidbody;
        m_Thrust = thrust;
    }
    public void SubsribeEvents()
    {
        m_TargetIndicatorLogic.OnTargetReleased += Jump;
    }

    public void UnsubscribeEvents()
    {
        m_TargetIndicatorLogic.OnTargetReleased -= Jump;
    }

    private void Jump(Vector3 vector)
    {
        m_Rigidbody.AddForce(vector * m_Thrust);
    }
}
