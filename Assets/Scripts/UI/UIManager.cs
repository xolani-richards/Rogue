using System;
using UnityEngine;
using Matso.Events;
using ONI.Menus;

public class UIManager: MonoBehaviour, IObserver
{
    [SerializeField] SelectionPopup selectionPopUpPrefab;
    [SerializeField] GameOverUI gameOverPrefab;
    SelectionPopup popup;
    GameManager gameManager;
    GameObject gameOverMenu;

    void Awake()
    {
        ServiceLocator.Register<UIManager>(this);
        // if (!result) Destroy(gameObject);

        EventBus.Register(EventKey.HERO_DIED, this);
        EventBus.Register(EventKey.GAME_PAUSED, this);
    }

    void OnDestroy() 
    {
        EventBus.Unregister(EventKey.HERO_DIED, this);
        EventBus.Unregister(EventKey.GAME_PAUSED, this);    
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

    public void OnHeroDied ()
    {
        gameOverMenu = Instantiate(gameOverPrefab, transform).gameObject;
    }

    void OnClose()
    {
        Debug.Log("Close called");
        gameManager.SwitchState(GameState.PLAYING);
    }

    void OnGamePaused ()
    {
        PauseMenu.Open();
    }

    public void OnNotify(EventKey type, object data)
    {
        if(type == EventKey.HERO_DIED) OnHeroDied ();
        else if(type == EventKey.GAME_PAUSED) OnGamePaused ();
    }
}