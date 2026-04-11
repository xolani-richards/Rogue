using UnityEngine;

public class PointManager:MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] int nextLevelRequirement;
    [SerializeField] int currentLevel;
    [SerializeField] int multiplier = 75;
    void Awake()
    {
        points = 0;
        currentLevel = 1;
        nextLevelRequirement = multiplier;
    }

    public void AddPoints(int value)
    {
        if(value <= 0) return;
        points += value;
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