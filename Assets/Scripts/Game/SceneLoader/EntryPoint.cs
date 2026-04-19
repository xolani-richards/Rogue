using UnityEngine;

public class EntryPoint: MonoBehaviour
{
    void Start()
    {
        SceneController.Instance.UpdateSceneIndex(SceneDatabase.Slots.Boot, SceneDatabase.Scenes.Boot);
        SceneController.Instance.NewTransition()
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu)
            .Unload(SceneDatabase.Slots.Boot)
            .WithOverlay()
            .Perform();
    }
}