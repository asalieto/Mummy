using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance == null)
        {
            GameObject gameManager = new GameObject("GameManager");
            gameManager.AddComponent<GameManager>();
        }
        GameManager.Instance.Init();
    }
}
