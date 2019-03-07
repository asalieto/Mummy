using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<MummyController> m_mummies;
    [SerializeField] private List<HumanController> m_humans;
    [SerializeField] private List<GemController> m_gems;
    [SerializeField] private float m_maxRandomRange;
    
    public int RemainingHumans { get { return m_remainingHumans; } }
    private int m_remainingHumans;
    public int RemainingGems { get { return m_remainingGems; } }
    private int m_remainingGems;

    private bool m_movesActive;

    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        m_movesActive = false;
    }

    public void Init()
    {
        m_movesActive = true;

        InitGems();
        InitHumans();
        InitMummies();

        GameManager.Instance.UpdateUI();
    }

    private void InitMummies()
    {
        for (int i = 0; i < m_mummies.Count; i++)
        {
            if (m_mummies[i] != null)
            {
                m_mummies[i].Init();
            }
        }
    }

    private void StopMummies()
    {
        for (int i = 0; i < m_mummies.Count; i++)
        {
            if (m_mummies[i] != null)
            {
                m_mummies[i].Stop();
            }
        }
    }

    private void InitHumans()
    {
        m_remainingHumans = 0;
        for (int i = 0; i < m_humans.Count; i++)
        {
            if (m_humans[i] != null)
            {
                m_humans[i].Init();
                m_humans[i].OnGemCollected += OnGemCollected;
                m_humans[i].OnDestroyed += OnHumanDestroyed;

                m_remainingHumans++;
            }
        }
    }

    private void StopHumans()
    {
        for (int i = 0; i < m_humans.Count; i++)
        {
            if (m_humans[i] != null)
            {
                m_humans[i].Stop();
            }
        }
    }

    private void InitGems()
    {
        m_remainingGems = 0;
        for (int i = 0; i < m_gems.Count; i++)
        {
            if (m_gems[i] != null)
            {
                m_remainingGems++;
            }
        }
    }

    private void OnGemCollected()
    {
        m_remainingGems--;
        GameManager.Instance.UpdateUI();

        if (m_remainingGems == 0)
        {
            StopMummies();
            StopHumans();

            m_movesActive = false;
            GameManager.Instance.EndGame(true);
        }
    }

    private void OnHumanDestroyed()
    {
        m_remainingHumans--;
        GameManager.Instance.UpdateUI();

        if (m_remainingHumans == 0)
        {
            StopMummies();
            StopHumans();

            m_movesActive = false;
            GameManager.Instance.EndGame(false);
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
        if (m_movesActive)
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
}
