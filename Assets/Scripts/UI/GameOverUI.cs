using UnityEngine;

public class GameOverUI : MonoBehaviour 
{
    public void OnRetry ()
    {
        GameManager.Instance.ReloadLevel();
    }

    public void OnReturn ()
    {
        Debug.Log("ALSO CLICKED");
        GameManager.Instance.ReturnToMainMenu();
    }
}