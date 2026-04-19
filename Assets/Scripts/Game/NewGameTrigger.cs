using UnityEngine;

public class NewGameTrigger: MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField, TextArea] string description;

    void Start()
    {
        UIManager ui = ServiceLocator.Get<UIManager>();
        ui?.ShowPopup(title, description, () => StartCoroutine(ServiceLocator.Get<FadeScreen>().FadeToClear(2f)));        
    }
}