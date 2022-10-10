using System;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private Collider2D m_Collider2D;

    public event Action OnPlayerGrounded;
    public event Action OnPlayerUngrounded;

    private const string GROUND = "Ground";

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(GROUND))
        {
            OnPlayerGrounded?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(GROUND))
        {
            OnPlayerUngrounded?.Invoke();
        }
    }
}
