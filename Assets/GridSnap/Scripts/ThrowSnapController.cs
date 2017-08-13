using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ThrowSnapController : MonoBehaviour
{
    private VRTK_InteractableObject m_interactableObjectScript;

    private GridSnapManager m_gridSnapManager;

    private GameObject m_cube;
    private GameObject m_snappingObject;

    private bool m_isSnapping = false;
    private Rigidbody m_rigidBody = null;
    
    private void Start()
    {
        m_gridSnapManager = FindObjectOfType<GridSnapManager>();
        Debug.Assert(m_gridSnapManager != null, "GridSnapManager doesn't eixst in current scene!");

        m_cube = transform.Find("Cube").gameObject;

        m_interactableObjectScript = GetComponent<VRTK_InteractableObject>();
        Debug.Assert(m_interactableObjectScript != null, "VRTK_InteractableObejct doesn't exist on " + transform.name);

        m_interactableObjectScript.InteractableObjectUngrabbed += OnThrow;
    }

    private void Update()
    {
        if (!m_isSnapping)
            return;

        Vector3 snapPos = m_gridSnapManager.GetSnapPos(transform.position);
        m_snappingObject.transform.position = snapPos;
        Debug.Log(snapPos.y);
    }

    private void OnThrow(object sender, InteractableObjectEventArgs e)
    {
        m_snappingObject = Object.Instantiate(m_cube);
        m_isSnapping = true;
        SetRealPlatformGraphics(false);
    }

    private void SetRealPlatformGraphics(bool isOn)
    {
        m_cube.SetActive(isOn);
    }
}
