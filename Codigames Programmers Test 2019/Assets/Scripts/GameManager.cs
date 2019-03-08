using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private UIController m_uiController;
    private CameraController m_cameraController;
    private PlayerController m_playerController;
    
    public void Init()
    {
        m_uiController = FindObjectOfType<UIController>();
        m_cameraController = FindObjectOfType<CameraController>();
        m_playerController = FindObjectOfType<PlayerController>();
        
        m_uiController.ShowStartMenu();
    }
    public void StartGame()
    {
        m_uiController.HideStartMenu();
        m_uiController.ShowInGameHUD();

        m_playerController.Init();
        m_cameraController.EnableMovement(true);
    }

    public void EndGame(bool victory)
    {
        m_cameraController.EnableMovement(false);

        m_uiController.HideInGameHUD();

        if (victory)
        {
            m_uiController.ShowWinPopup();
        }
        else
        {
            m_uiController.ShowLosePopup();
        }
    }

    public void UpdateUI()
    {
        m_uiController.SetGems(m_playerController.RemainingGems);
        m_uiController.SetHumanLives(m_playerController.RemainingHumans);
    }
    
    public void RestartGame()
    {
        //TODO: Instead of reloading the scene, re-instantiate humans, gems and init the mummies.
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
