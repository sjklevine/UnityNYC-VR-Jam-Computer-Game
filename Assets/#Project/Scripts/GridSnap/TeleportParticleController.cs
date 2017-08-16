using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportParticleController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_teleportAppear;
    [SerializeField]
    private GameObject m_teleportBurst;

    private void Start()
    {
        m_teleportAppear.SetActive(false);
        m_teleportBurst.SetActive(false);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
            PlayTeleportParticles();
#endif
    }

    public void PlayTeleportParticles()
    {
        StartCoroutine(DoTeleportParticleSequence());
    }
    
    private IEnumerator DoTeleportParticleSequence()
    {
        m_teleportAppear.SetActive(true);
        yield return new WaitForSeconds(2f);
        m_teleportAppear.SetActive(false);
        m_teleportBurst.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_teleportBurst.SetActive(false);
    }

}
