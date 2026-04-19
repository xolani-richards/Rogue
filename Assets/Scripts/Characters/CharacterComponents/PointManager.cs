using System;
using UnityEngine;

public class PointManager: MonoBehaviour
{
    public static PointManager instance;
    [field: SerializeField] public int points { get; private set; }
    [field: SerializeField] public int nextLevelRequirement { get; private set; }
    [SerializeField] int currentLevel;
    [SerializeField] int multiplier = 75;

    public Action onPointsUpdated;

    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);

        points = 0;
        currentLevel = 1;
        nextLevelRequirement = multiplier;
    }

    public void AddPoints(int value)
    {
        if(value <= 0) return;
        points += value;
        onPointsUpdated?.Invoke();
        if(points >= nextLevelRequirement) LevelUp();
    }

    void LevelUp()
    {
        currentLevel += 1;
        nextLevelRequirement = currentLevel * multiplier;
        Debug.Log("LEVEL UP!");
        if(points >= nextLevelRequirement) LevelUp();
    }
}