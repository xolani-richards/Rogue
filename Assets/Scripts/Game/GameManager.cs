using System;
using Matso.Events;
using UnityEngine;

public enum GameState { LOADING, MENU, PAUSED, PLAYING }

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] int frameRate = 60;
    [field: SerializeField] public GameState gameState { get; protected set; } = GameState.LOADING;
    public Action<GameState> onStateUpdated;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);

        ServiceLocator.Register<GameManager>(this);
        Application.targetFrameRate = frameRate;
    }

    public void SwitchState(GameState state)
    {
        Debug.Log($"GameManger: SwitchState Called: {state}");
        switch (state)
        {
            case GameState.MENU: OnMenu(); break;
            case GameState.PLAYING: OnPlaying(); break;
            case GameState.PAUSED: OnPaused(); break;
            case GameState.LOADING: OnLoading(); break;
        }
    }

    public void LoadNewGame()
    {
        SceneController.Instance.NewTransition()
            .Load(SceneDatabase.Slots.GamePlayCore, SceneDatabase.Scenes.GamePlayCore)
            .Load(SceneDatabase.Slots.SampleScene, SceneDatabase.Scenes.SampleScene, setActive: true)
            .Unload(SceneDatabase.Slots.Menu)
            .WithOverlay()
            .WithLoadingMenu()
            .Perform();
    }

    public void ReturnToMainMenu()
    {
        SwitchState(GameState.LOADING);
        SceneController.Instance.NewTransition()
            .Unload(SceneDatabase.Slots.SampleScene)
            .Unload(SceneDatabase.Slots.GamePlayCore)
            .Load(SceneDatabase.Slots.Menu, SceneDatabase.Scenes.MainMenu, setActive: true)
            .WithOverlay()
            .WithClearUnusedAssets()
            .Perform();
    }

    public void ReloadLevel()
    {
        SwitchState(GameState.LOADING);
        SceneController.Instance.NewTransition()
        .Load(SceneDatabase.Slots.GamePlayCore, SceneDatabase.Scenes.GamePlayCore)
        .Load(SceneDatabase.Slots.SampleScene, SceneDatabase.Scenes.SampleScene, setActive: true)
        .WithClearUnusedAssets()
        .WithOverlay()
        .WithLoadingMenu()
        .Perform();

    }

    private void OnLoading()
    {
        gameState = GameState.LOADING;
        Time.timeScale = 1f;
    }

    private void OnPaused()
    {
        gameState = GameState.PAUSED;
        Time.timeScale = 0;
        EventBus.Publish(EventKey.GAME_PAUSED, null);
        onStateUpdated?.Invoke(gameState);
    }

    private void OnPlaying()
    {
        gameState = GameState.PLAYING;
        Time.timeScale = 1;
        onStateUpdated?.Invoke(gameState);
        EventBus.Publish(EventKey.GAME_PLAYING, null);
    }

    private void OnMenu()
    {
        Time.timeScale = 0;
        gameState = GameState.MENU;
        onStateUpdated?.Invoke(gameState);
    }
}