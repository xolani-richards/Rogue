using System;
using Matso.Events;
using UnityEngine;

public class HeroController : CharacterController, IObserver
{
    public enum Mode {UI, IN_GAME}
    public Mode mode = Mode.UI;
    public bool cast;
    public Action onNext;
    public Action onPrevious;

    PlayerInput controls;

    public void OnNotify(EventKey type, object data)
    {
        if(type == EventKey.GAME_PAUSED) OnMenuMode();
        else if(type == EventKey.GAME_PLAYING) OnGamePlayMode();
    }

    void Awake()
    {
        controls = new ();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        
        controls.Player.Attack.performed += ctx => attackInput = true;
        controls.Player.Attack.canceled += ctx => attackInput = false; 
        controls.Player.Sprint.performed += ctx => walkInput = true;
        controls.Player.Sprint.canceled += ctx => walkInput = false;
        controls.Player.Cast.performed += ctx => cast = true;
        controls.Player.Cast.canceled += ctx => cast = false;

        controls.Player.Next.performed += ctx => onNext?.Invoke();
        controls.Player.Previous.performed += ctx => onPrevious?.Invoke();
        controls.Player.Pause.performed += ctx => GameManager.Instance.SwitchState(GameState.PAUSED);
        
        EventBus.Register(EventKey.GAME_PAUSED, this);
        EventBus.Register(EventKey.GAME_PLAYING, this);
        OnGamePlayMode();
    }

    void OnDestroy()
    {
        EventBus.Unregister(EventKey.GAME_PAUSED, this);
        EventBus.Unregister(EventKey.GAME_PLAYING, this);
    }

    private void OnMenuMode ()
    {
        controls.UI.Enable();
        controls.Player.Disable();
        mode = Mode.UI;
    }

    private void OnGamePlayMode()
    {
        controls.UI.Disable();
        controls.Player.Enable();
        mode = Mode.IN_GAME;
    }

    private void OnGameStateUpdated(GameState state)
    {
        if(state != GameState.PLAYING) controls.Player.Disable();
        else controls.Player.Enable();
    }
}
