using System;
using UnityEngine;

public enum GameState { LOADING, MENU, PAUSED, PLAYING }

public class GameManager: MonoBehaviour
{
    [SerializeField] int frameRate = 60;
    [field: SerializeField] public GameState gameState { get; protected set; }

    void Awake()
    {
        Application.targetFrameRate = frameRate;
    }

    public void SwitchState(GameState state)
    {
        switch (state)
        {
            case GameState.MENU: OnMenu(); break;
            case GameState.PLAYING: OnPlaying(); break;
            case GameState.PAUSED: OnPaused(); break;
        }
    }

    private void OnPaused()
    {
        gameState = GameState.PAUSED;
        Time.timeScale = 0;
    }

    private void OnPlaying()
    {
        gameState = GameState.PLAYING;
        Time.timeScale = 1;
    }

    private void OnMenu()
    {
        gameState = GameState.PLAYING;
    }
}