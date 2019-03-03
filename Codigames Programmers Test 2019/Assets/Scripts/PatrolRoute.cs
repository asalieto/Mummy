using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    [SerializeField]
    private float m_waypointRadius = 0.5f;
    [SerializeField]
    private bool m_circular;
    [SerializeField]
    private Transform[] m_waypoints;
    
    public bool IsCircular { get { return m_circular; } }
    public Transform[] Waypoints { get { return m_waypoints; } }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (m_waypoints != null)
        {
            for (int i = 0; i < m_waypoints.Length; i++)
            {
                if (m_waypoints[i] != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(m_waypoints[i].position, m_waypointRadius);

                    int nextWaypoint = i + 1;

                    if (m_circular)
                    {
                        nextWaypoint = nextWaypoint % m_waypoints.Length;
                    }

                    if (nextWaypoint < m_waypoints.Length && m_waypoints[nextWaypoint] != null)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(m_waypoints[i].position, m_waypoints[nextWaypoint].position);
                    }
                }
            }
        }
#endif
    }
}
