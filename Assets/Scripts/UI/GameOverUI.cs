using UnityEngine;

public class GameOverUI : MonoBehaviour 
{
    public void OnRetry ()
    {
        Debug.Log("CLICKED");
        // LevelLoader.LoadScene("SampleScene");
    }

    public void OnReturn ()
    {
        Debug.Log("ALSO CLICKED");
        // LevelLoader.LoadScene("SampleScene");
    }
}