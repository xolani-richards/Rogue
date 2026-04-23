using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpiritStonesUI : MonoBehaviour {
    [SerializeField] Image fillBar;
    [SerializeField] TMP_Text points;
    [SerializeField] TMP_Text targetPoints;
    [SerializeField] PointManager pointManager;

    public void Start ()
    {
        pointManager = PointManager.instance;
        pointManager.onPointsUpdated += OnUpdate;
        pointManager.onLevelUp += OnLevelUp;
        OnUpdate();
    }

    private void OnDestroy() 
    {
        pointManager.onPointsUpdated -= OnUpdate;
        pointManager.onLevelUp -= OnLevelUp;
    }

    void OnUpdate()
    {
        points.text = pointManager.points.ToString();
        targetPoints.text = pointManager.nextLevelRequirement.ToString();
        fillBar.fillAmount = pointManager.points / pointManager.nextLevelRequirement;
    }

    void OnLevelUp() => OnUpdate();
}