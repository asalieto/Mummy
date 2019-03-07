using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject m_startPanel;
    [SerializeField] private GameObject m_helpPanel;

    public void ShowMenu()
    {
        m_startPanel.SetActive(true);
        m_helpPanel.SetActive(false);
    }

    public void HideMenu()
    {
        m_startPanel.SetActive(false);
        m_helpPanel.SetActive(false);
    }

    public void PlayButton()
    {
        GameManager.Instance.StartGame();
    }

    public void HelpButton()
    {
        m_startPanel.SetActive(false);
        m_helpPanel.SetActive(true);
    }

    public void BackButton()
    {
        m_startPanel.SetActive(true);
        m_helpPanel.SetActive(false);
    }
}
