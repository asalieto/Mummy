using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_fpsCounter;
    [SerializeField] private TMP_Text m_gemsLeft;
    [SerializeField] private List<GameObject> m_humansLeft;
    [SerializeField] private float m_fpsInterval;

    [Header("Menus & Popups")]
    [SerializeField] private MenuController m_menuController;
    [SerializeField] private GameObject m_gameHud;
    [SerializeField] private GameObject m_winPopup;
    [SerializeField] private GameObject m_losePopup;

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
    
    private void HideAllPopups()
    {
        HideStartMenu();
        HideWinPopup();
        HideLosePopup();
        HideInGameHUD();
    }

    public void ShowStartMenu()
    {
        HideAllPopups();

        m_menuController.gameObject.SetActive(true);
        m_menuController.ShowMenu();
    }

    public void HideStartMenu()
    {
        m_menuController.gameObject.SetActive(false);
    }
    
    public void ShowInGameHUD()
    {
        m_gameHud.SetActive(true);
    }

    public void HideInGameHUD()
    {
        m_gameHud.SetActive(false);
    }

    public void ShowWinPopup()
    {
        m_winPopup.SetActive(true);
    }

    public void HideWinPopup()
    {
        m_winPopup.SetActive(false);
    }

    public void ShowLosePopup()
    {
        m_losePopup.SetActive(true);
    }

    public void HideLosePopup()
    {
        m_losePopup.SetActive(false);
    }

    public void SetGems(int remaining)
    {
        m_gemsLeft.text = remaining.ToString();
    }

    public void SetHumanLives(int remaining)
    {
        bool state;

        for (int i = 0; i < m_humansLeft.Count; i++)
        {
            if (m_humansLeft[i] != null)
            {
                state = i < remaining;

                if (m_humansLeft[i].gameObject.activeSelf != state)
                {
                    m_humansLeft[i].SetActive(state);
                }
            }
        }
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }
}
