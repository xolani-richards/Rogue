using System;
using UnityEngine;

public enum GameState { LOADING, MENU, PAUSED, PLAYING }

public class GameManager: MonoBehaviour
{
    [SerializeField] int frameRate = 60;
    [field: SerializeField] public GameState gameState { get; protected set; } = GameState.LOADING;
    public Action<GameState> onStateUpdated;

    void Awake()
    {
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

    private void OnLoading()
    {
        gameState = GameState.LOADING;
        Debug.Log("Loading");
    }

    private void OnPaused()
    {
        gameState = GameState.PAUSED;
        Time.timeScale = 0;
        onStateUpdated?.Invoke(gameState);
    }

    private void OnPlaying()
    {
        gameState = GameState.PLAYING;
        Time.timeScale = 1;
        onStateUpdated?.Invoke(gameState);
    }

    private void OnMenu()
    {
        Time.timeScale = 0;
        gameState = GameState.MENU;
        onStateUpdated?.Invoke(gameState);
    }
}