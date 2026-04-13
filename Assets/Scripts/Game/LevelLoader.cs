using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader :MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadScene () {
        Scene active = SceneManager.GetActiveScene();
        // LoadScene(active.name);
    }

    public static void LoadScene(string sceneName)
    {
        FadeScreen.instance.FadeOut(0);
        SceneManager.LoadScene(sceneName);
        FadeScreen.instance.FadeIn(2f);

    }
}