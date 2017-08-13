using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SnapController : MonoBehaviour
{
    private VRTK_InteractableObject m_interactableObjectScript;

    private GridSnapManager m_gridSnapManager;

    //private GameObject m_cube;
    [SerializeField]
    private GameObject[] m_visuals;

    private GameObject m_snappingObject;

    private bool m_isSnapping = false;

    private Rigidbody m_rigidBody = null;

    private void Start()
    {
        m_gridSnapManager = FindObjectOfType<GridSnapManager>();
        Debug.Assert(m_gridSnapManager != null, "GridSnapManager doesn't eixst in current scene!");

        //m_cube = transform.Find("Cube").gameObject;
        m_rigidBody = GetComponent<Rigidbody>();
        m_rigidBody.isKinematic = true;
        m_rigidBody.useGravity = false;

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
        //m_snappingObject = Object.Instantiate(m_cube);
        CreateVisualDuplicates();
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
        //m_cube.SetActive(isOn);
        for(int i = 0; i < m_visuals.Length; ++i)
        {
            m_visuals[i].SetActive(isOn);
        }
    }

    private void CreateVisualDuplicates()
    {
        m_snappingObject = new GameObject();
		m_snappingObject.transform.localScale = transform.lossyScale;

        for(int i = 0; i < m_visuals.Length; ++i)
        {
            Object.Instantiate(m_visuals[i], m_snappingObject.transform);
        }
    }

}
