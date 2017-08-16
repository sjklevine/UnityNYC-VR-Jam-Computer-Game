using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TreeParticleController : MonoBehaviour
{
    private Vector3 m_leafSpawnPosOffSet = new Vector3(-1f, 3f, -0.85f);
    [SerializeField]
    private GameObject m_leafDropParticlePrefab;

    private VRTK_InteractableObject m_interactableObjectScript;

    private void Strat()
    {
        Debug.Assert(m_leafDropParticlePrefab != null, "Leaf drop particle prefab not set on " + transform.name);

        m_interactableObjectScript = GetComponent<VRTK_InteractableObject>();
        m_interactableObjectScript.InteractableObjectGrabbed += OnGrab;
    }

    private void OnGrab(object sender, InteractableObjectEventArgs e)
    {
        Instantiate(m_leafDropParticlePrefab, transform.position + m_leafSpawnPosOffSet, Quaternion.identity);
    }
}
