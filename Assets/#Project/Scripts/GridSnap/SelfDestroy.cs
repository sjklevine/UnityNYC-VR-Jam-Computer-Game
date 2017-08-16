using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField]
    private float m_aliveTime = 5f;

    private void Start()
    {
        Destroy(gameObject, m_aliveTime);
    }
}
