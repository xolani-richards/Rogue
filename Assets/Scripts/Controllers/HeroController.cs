using System;
using UnityEngine;

public class HeroController : CharacterController
{
    GameManager gameManager;
    PlayerInput inputs;

    void Awake()
    {
        inputs = new ();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        
        inputs.Player.Attack.performed += ctx => attackInput = true;
        inputs.Player.Attack.canceled += ctx => attackInput = false; 
        inputs.Player.Sprint.performed += ctx => walkInput = true;
        inputs.Player.Sprint.canceled += ctx => walkInput = false;
        inputs.Player.Enable();
    }

    void Start()
    {
        gameManager = ServiceLocator.Get<GameManager>();
        gameManager.onStateUpdated += OnGameStateUpdated;
    }

    void OnDestroy()
    {
        gameManager.onStateUpdated -= OnGameStateUpdated;
    }

    private void OnGameStateUpdated(GameState state)
    {
        if(state != GameState.PLAYING) inputs.Player.Disable();
        else inputs.Player.Enable();
    }
}
