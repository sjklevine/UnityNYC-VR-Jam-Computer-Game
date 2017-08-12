using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SnapController : MonoBehaviour
{
    private GameObject m_gameObject;
    private VRTK_InteractableObject m_interactableObjectScript;

    private GridSnapManager m_gridSnapManager;

    private GameObject m_cube;
    private GameObject m_snappingObject;

    private bool m_isSnapping = false;

    private void Start()
    {
        m_gridSnapManager = FindObjectOfType<GridSnapManager>();
        Debug.Assert(m_gridSnapManager != null, "GridSnapManager doesn't eixst in current scene!");

        m_cube = transform.Find("Cube").gameObject;

        m_interactableObjectScript = GetComponent<VRTK_InteractableObject>();
        Debug.Assert(m_interactableObjectScript != null, "VRTK_InteractableObejct doesn't exist on " + transform.name);

        m_interactableObjectScript.InteractableObjectGrabbed += OnGrab;
        m_interactableObjectScript.InteractableObjectUngrabbed += OnStopGrab;
    }

    private void Update()
    {
        if (!m_isSnapping)
            return;

        Vector3 snapPos = m_gridSnapManager.GetSnapPos(transform.position);
        m_snappingObject.transform.position = snapPos;
    }

    private void OnGrab(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("grabbed");
        m_snappingObject = Object.Instantiate(m_cube);
        m_isSnapping = true;
        SetRealPlatformGraphics(false);
    }

    private void OnStopGrab(object sender, InteractableObjectEventArgs e)
    {
        Debug.Log("Ungrabbed");
        m_isSnapping = false;
        transform.position = m_snappingObject.transform.position;
        transform.rotation = m_snappingObject.transform.rotation;
        Destroy(m_snappingObject);
        SetRealPlatformGraphics(true);
    }

    private void SetRealPlatformGraphics(bool isOn)
    {
        m_cube.SetActive(isOn);
    }
}
