using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_fpsCounter;
    [SerializeField]
    private float m_fpsInterval;

    private float m_fpsCurrentInterval;

    private void Update()
    {
        m_fpsCurrentInterval += Time.deltaTime;

        if (m_fpsCurrentInterval >= m_fpsInterval)
        {
            m_fpsCurrentInterval -= m_fpsInterval;
            m_fpsCounter.text = "FPS: " + System.Math.Round(1f / Time.deltaTime, 0);
        }
    }
}
