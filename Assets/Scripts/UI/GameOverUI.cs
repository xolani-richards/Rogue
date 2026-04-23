using UnityEngine;

public class GameOverUI : MonoBehaviour 
{
    public void OnRetry ()
    {
        GameManager.Instance.ReloadLevel();
        Destroy(gameObject, 0.2f);
    }

    public void OnReturn ()
    {
        GameManager.Instance.ReturnToMainMenu();
        Destroy(gameObject, 0.2f);
    }
}