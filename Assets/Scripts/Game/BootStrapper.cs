using UnityEngine;
using UnityEngine.SceneManagement;
public static class BootStrapper
{
    const string SceneName = "Core";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        // traverse loaded scene
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)
        {
            var scene = SceneManager.GetSceneAt(sceneIndex);
            if (scene.name == SceneName) return;
        }

        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);

    }
    
    // public static void LoadSystems () => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
}