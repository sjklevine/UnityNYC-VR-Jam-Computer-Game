using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidescrollerEndingScript : MonoBehaviour
{
	public SimpleTimedToggleVisibilty endgameText;

    [SerializeField]
    private GameObject m_fireWorksPrefab;
    [SerializeField]
    private float m_xOffset = 1f;
    [SerializeField]
    private float m_yOffset = 1f;
    private const float m_fireWorksInterval = 1f;
    private const int m_maxFireWorksCount = 5;

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("Player")) {
            endgameText.BeginToggling();
            endgameText.GetComponent<AudioSource>().Play();

            Vector3 playerPos = other.transform.position;
            PlayFireWorks(playerPos);
		}
	}

    private void PlayFireWorks(Vector3 playerPos)
    {
        if(m_fireWorksPrefab == null)
        {
            Debug.LogWarning("Fire works prefab not set on " + transform.name);
            return;
        }

        StartCoroutine(DoFireWorkSequence(playerPos));
    }

    private IEnumerator DoFireWorkSequence(Vector3 playerPos)
    {
        float xPos = playerPos.x;
        float yPos = playerPos.y;
        float xRangeMax = xPos + m_xOffset;
        float xRangeMin = xPos - m_xOffset;
        float yRangeMax = yPos + m_yOffset;
        float yRangeMin = yPos + m_yOffset / 2;

        Quaternion fireWorkRotation = m_fireWorksPrefab.transform.rotation;
        float z = transform.position.z;
        Vector3 scale = m_fireWorksPrefab.transform.localScale / 100;

        int i = 0;
        while(i < m_maxFireWorksCount)
        {
            float x = Random.Range(xRangeMin, xRangeMax);
            float y = Random.Range(yRangeMin, yRangeMax);
            Vector3 pos = new Vector3(x, y, z);
            GameObject fireWorks = Instantiate(m_fireWorksPrefab, pos, fireWorkRotation);
            fireWorks.transform.localScale = scale;
            yield return new WaitForSeconds(m_fireWorksInterval);
            ++i;
        }
    }
}
