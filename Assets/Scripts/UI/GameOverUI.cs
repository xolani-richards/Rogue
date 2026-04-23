using UnityEngine;

public class GameOverUI : MonoBehaviour 
{
    void Start()
    {
        Time.timeScale = 0.25f;
    }
    public void OnRetry ()
    {
        GameManager.Instance.ReloadLevel();
        Time.timeScale = 1f;
        Destroy(gameObject, 0.2f);
    }

    public void OnReturn ()
    {
        GameManager.Instance.ReturnToMainMenu();
        Time.timeScale = 1f;
        Destroy(gameObject, 0.2f);
    }
}