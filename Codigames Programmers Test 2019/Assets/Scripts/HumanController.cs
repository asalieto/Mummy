using System;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private NavMeshAgent m_navMeshAgent;

    private bool m_isMoving;
    
    public Action OnDestroyed;
    public Action OnGemCollected;

    public void Init()
    {
        m_navMeshAgent.isStopped = false;

        m_animator.SetBool("Grounded", true);
    }

    public void Stop()
    {
        m_navMeshAgent.isStopped = true;
    }

    public void MoveTo(Vector3 position)
    {
        m_isMoving = true;

        m_navMeshAgent.destination = position;
        m_navMeshAgent.isStopped = false;

        m_animator.SetFloat("MoveSpeed", 15f);
    }

    private void Die()
    {
        if (OnDestroyed != null)
        {
            OnDestroyed();
        }
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
        else if (collider.tag == "Collectable")
        {
            //GemController gem = collider.GetComponent<GemController>();
            //Destroy(gem.gameObject);

            Destroy(collider.gameObject);

            if (OnGemCollected != null)
            {
                OnGemCollected();
            }
        }
    }
}
