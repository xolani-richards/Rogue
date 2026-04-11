using UnityEngine;
public static class BootStrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadSystems () => Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Systems")));
}