using UnityEngine;
using UnityEngine.AI;

public class MummyController : MonoBehaviour
{
    [SerializeField] private BoxCollider m_collider;
    [SerializeField] private NavMeshAgent m_navMeshAgent;
    [SerializeField] private PatrolRoute m_patrolWaypoints;
    [SerializeField] private Animator m_animator;

    private int m_destinationIndex = 0;
    private bool m_activePatrol;
    private bool m_foward;
    
    private void Start()
    {
        Stop();
    }
    
    public void Init()
    {
        if (m_patrolWaypoints.Waypoints.Length == 0)
        {
            return;
        }
        m_foward = true;
        m_activePatrol = true;
        m_navMeshAgent.isStopped = true;
        m_animator.SetBool("Idle", false);

        m_navMeshAgent.Warp(m_patrolWaypoints.Waypoints[m_destinationIndex].position);
        GoToNextWaypoint();
    }

    public void Stop()
    {
        m_navMeshAgent.isStopped = true;
        m_animator.SetBool("Idle", true);
    }

    private void GoToNextWaypoint()
    {
        if (m_patrolWaypoints.IsCircular)
        {
            m_destinationIndex = (m_destinationIndex + 1) % m_patrolWaypoints.Waypoints.Length;
        }
        else
        {
            if (m_foward && m_destinationIndex + 1 >= m_patrolWaypoints.Waypoints.Length)
            {
                m_foward = false;
            }
            else if (m_destinationIndex - 1 < 0)
            {
                m_foward = true;
            }

            m_destinationIndex = m_foward ? m_destinationIndex + 1 : m_destinationIndex - 1;
        }

        m_navMeshAgent.destination = m_patrolWaypoints.Waypoints[m_destinationIndex].position;
    }

    private void Update()
    {
        if (m_activePatrol && !m_navMeshAgent.pathPending && m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            GoToNextWaypoint();
        }
    }
}
