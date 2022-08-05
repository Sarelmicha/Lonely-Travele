using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicatorLogic 
{
    private Transform m_TargetTransform;
    private Transform m_BaseTransform;
    private Vector2 m_MousePosition;
    private bool m_IsDragging;
    private float m_Radius;

    /// <summary>
    /// Invoke when the player relase the target on a certin location. 
    /// </summary>
    public event Action<Vector3> OnTargetReleased;

    public TargetIndicatorLogic(Transform targetTransform, Transform baseTransform, float radius)
    {
        m_TargetTransform = targetTransform;
        m_BaseTransform = baseTransform;
        m_Radius = radius;
    }

    public void TryUpdatePosition()
    {
        if (m_IsDragging)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        m_MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(m_MousePosition, m_BaseTransform.position) <= m_Radius)
        {
            SetPosition(m_MousePosition);
        }
        else
        {
            SetPosition(Vector3.Normalize(m_MousePosition) * m_Radius);
        }
    }
    private void SetPosition(Vector3 position)
    {
        m_TargetTransform.position = position;
    }

    public void OnMouseUp()
    {
        m_IsDragging = false;
        OnTargetReleased?.Invoke(-(Vector3.Normalize(m_TargetTransform.position) * Vector2.Distance(m_TargetTransform.position, m_BaseTransform.position)));
        SetPosition(m_BaseTransform.position);
    }

    public void OnMouseDown()
    {
        Debug.Log("imhere");
        m_IsDragging = true;
    }
}
