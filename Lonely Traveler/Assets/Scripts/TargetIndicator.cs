using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private CircleCollider2D m_Base;
    private float m;
    private Collider2D m_Collider;
    private bool m_IsDragging;
    private Vector2 m_MousePosition;

    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (m_IsDragging)
        {
            Debug.Log("im here");
            m_MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log($"m_MousePosition {m_MousePosition}");
            Debug.Log($"transform.position {transform.position}");
            Debug.Log($"m_Base.radius {m_Base.radius}");

            if (Vector2.Distance(m_MousePosition, m_Base.transform.position) <= m_Base.radius)
            {
                transform.position = m_MousePosition;
            }
            else 
            {
                transform.position = Vector3.Normalize(m_MousePosition) * m_Base.radius;
            }
        }
    }


    private void OnMouseDown()
    {
        m_IsDragging = true;
    }

    private void OnMouseUp()
    {
        m_IsDragging = false;
        transform.position = m_Base.transform.position;
    }
}
