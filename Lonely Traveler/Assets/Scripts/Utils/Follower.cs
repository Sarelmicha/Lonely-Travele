using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;
   
    void Update()
    {
        transform.position = m_Target.transform.position;
    }
}
