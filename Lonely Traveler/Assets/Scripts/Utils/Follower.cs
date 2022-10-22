using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;

    private void Update()
    {
        transform.position = m_Target.transform.position;
    }
}
