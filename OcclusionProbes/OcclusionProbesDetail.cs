#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionProbesDetail : MonoBehaviour {

	[Header("Resolution")]
    public int          m_XCount = 10;
    public int          m_YCount = 10;
    public int          m_ZCount = 10;

    [Header("Settings")]
    [Range(0, 1)]
    [Tooltip("Offsets rays away from the probe center to limit self occlusion. 1 will offset in all directions by the distance equal to the nearest probe in any direction.")]
    public float        m_RayOffset = 0.8f;

	void OnDrawGizmosSelected()
    {
        OcclusionProbes.DrawGizmos(transform, m_XCount, m_YCount, m_ZCount);
    }

    void OnValidate()
    {
        m_XCount = Mathf.Max(m_XCount, 2);
        m_YCount = Mathf.Max(m_YCount, 2);
        m_ZCount = Mathf.Max(m_ZCount, 2);
    }
}
#endif