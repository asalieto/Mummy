using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<HumanController> m_humans;
    [SerializeField]
    private float m_maxRandomRange;

    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < m_humans.Count; i++)
        {
            if (m_humans[i] != null)
            {
                m_humans[i].Init();
            }
        }
    }

    private void MoveHumans(Vector3 position)
    {
        float variationX, variationZ;
        Vector3 destination = new Vector3();

        for (int i = 0; i < m_humans.Count; i++)
        {
            if (m_humans[i] != null)
            {
                variationX = Random.Range(-m_maxRandomRange, m_maxRandomRange);
                variationZ = Random.Range(-m_maxRandomRange, m_maxRandomRange);

                destination.x = position.x + variationX;
                destination.y = position.y;
                destination.z = position.z + variationZ;

                m_humans[i].MoveTo(destination);
            }
        }
    }
    
    private void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown(0)))
        {

#if UNITY_EDITOR
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif (UNITY_ANDROID || UNITY_IPHONE)
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 destination = hit.point;
                MoveHumans(destination);
            }
        }
    }
}
