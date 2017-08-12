using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapManager : MonoBehaviour
{
    private const float m_gridUnitSize = 0.02f;

    public Vector3 GetSnapPos(Vector3 currentPos)
    {
        Vector3 snapPos = new Vector3();
        snapPos.x = Mathf.Round(currentPos.x / m_gridUnitSize) * m_gridUnitSize;
        snapPos.y = Mathf.Round(currentPos.y / m_gridUnitSize) * m_gridUnitSize;
        snapPos.z = Mathf.Round(currentPos.z / m_gridUnitSize) * m_gridUnitSize;
        return snapPos;
    }
}
