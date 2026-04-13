using System;
using UnityEngine;

public class UIManager: MonoBehaviour
{
    [SerializeField] SelectionPopup selectionPopUpPrefab;
    SelectionPopup popup;
    GameManager gameManager;

    void Awake()
    {
        bool result = ServiceLocator.Register<UIManager>(this);
        Debug.Log($"UIMANAGER: {result}");
        if (!result) Destroy(gameObject);
    }

    void Init()
    {
        gameManager = ServiceLocator.Get<GameManager>();
    }

    public void ShowPopup (string title, string content, Action onAccept, Action onReject = null)
    {
        if (popup != null) popup.OnReject();
        if (gameManager == null) Init();
        popup = Instantiate (selectionPopUpPrefab, transform);
        popup.Bind(title, content, () => { onAccept(); OnClose(); });
        gameManager.SwitchState(GameState.MENU);
    }

    void OnClose()
    {
        Debug.Log("Close called");
        gameManager.SwitchState(GameState.PLAYING);
    }
}