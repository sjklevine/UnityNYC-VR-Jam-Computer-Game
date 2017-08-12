using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapManager : MonoBehaviour
{
    private float m_gridUnitSize = 1f;

    public Vector2 GetSnapPos(float currentX, float currentY)
    {
        Vector2 snapPos = new Vector2();
        snapPos.x = Mathf.Round(currentX / m_gridUnitSize) * m_gridUnitSize;
        snapPos.y = Mathf.Round(currentY / m_gridUnitSize) * m_gridUnitSize;
        return snapPos;
    }
}
