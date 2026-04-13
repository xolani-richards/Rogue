using UnityEngine;

public class LevelManager:MonoBehaviour
{
    [SerializeField] GameState gameState;
    void Start()
    {
        GameManager gameManager = ServiceLocator.Get<GameManager>();
        UIManager uIManager = ServiceLocator.Get<UIManager>();
        gameManager.SwitchState(gameState);
    }
}