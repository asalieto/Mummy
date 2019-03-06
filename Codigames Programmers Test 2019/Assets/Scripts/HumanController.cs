using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private NavMeshAgent m_navMeshAgent;

    private bool m_isMoving;

    public void Init()
    {
        m_animator.SetBool("Grounded", true);
    }

    public void MoveTo(Vector3 position)
    {
        // Debug.Log("MoveTo: " + position);
        m_isMoving = true;

        m_navMeshAgent.destination = position;
        m_navMeshAgent.isStopped = false;

        m_animator.SetFloat("MoveSpeed", 15f);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (m_isMoving)
        {
            if (m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
            {
                if (!m_navMeshAgent.hasPath || Mathf.Abs(m_navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
                {
                    m_isMoving = false;
                    m_animator.SetFloat("MoveSpeed", 0f);
                }
            }
            else
            {
                m_isMoving = true;
            }
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Mummy")
        {
            Die();
        }
    }
}
