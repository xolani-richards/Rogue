using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelLoader :MonoBehaviour
{
    static List<string> menuScenes = new (){"MainMenu","Splash","Boot","GameplayCore"};
    static string gameplayScene = "GameplayCore";
    static string sceneToLoad;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadScene () {
        Debug.Log("RUNNING LOAD");
        Scene active = SceneManager.GetActiveScene();
        if(menuScenes.Contains(active.name)) return; 
        SceneManager.LoadScene(gameplayScene, LoadSceneMode.Additive);
    }

    public static void LoadScene(string sceneName)
    {
        FadeScreen.instance.FadeOut(0);
        SceneManager.LoadScene(sceneName);
        FadeScreen.instance.FadeIn(2f);
    }

    static IEnumerator<float> LoadLevel (string sceneName)
    {
        yield return 0f;
    }
}