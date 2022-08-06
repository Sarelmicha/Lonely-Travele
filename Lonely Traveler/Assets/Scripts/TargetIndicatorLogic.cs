using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicatorLogic 
{
    private Transform m_TargetTransform;
    private Transform m_BaseTransform;
    private Vector3 m_MousePosition;
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

       // Debug.Log(Vector3.Normalize(m_MousePosition));
       // Debug.Log(Vector3.Normalize(m_BaseTransform.position));

        if (Vector2.Distance(m_MousePosition, m_BaseTransform.position) <= m_Radius)
        {
            SetPosition(m_MousePosition);
        }
        else
        {
            var direction = m_MousePosition - m_BaseTransform.position;
            SetPosition(m_BaseTransform.position + (Vector3.Normalize(direction) * m_Radius));
        }
    }
    private void SetPosition(Vector3 position)
    {
        m_TargetTransform.position = position;
    }

    public void OnMouseUp()
    {       
        m_IsDragging = false;
        OnTargetReleased?.Invoke(m_BaseTransform.position - m_TargetTransform.position);
        SetPosition(m_BaseTransform.position);
    }

    public void OnMouseDown()
    {
        Debug.Log("imhere");
        m_IsDragging = true;
    }
}
