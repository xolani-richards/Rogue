using ONI.Menus;
using UnityEngine;

public class ShowMenuOnLoad : MonoBehaviour
{
    public enum MenuName { None, MAIN, INGAME }
    [SerializeField] MenuName menuName;

    void Start()
    {
        Debug.Log("Opening menu on Start");
        if (menuName == MenuName.MAIN) {
            SceneController.Instance.UpdateSceneIndex(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu);
            MainMenu.Open();
        }
        else if (menuName == MenuName.INGAME) InGameMenu.Open();
    }
}