using UnityEngine;
using UnityEngine.UI;
using ROGUE.Characters;

public class NPCHealthBar : MonoBehaviour 
{
    [SerializeField] Image healthBar;
    Health health;

    private void Start () {
        health = GetComponentInParent<Health>();
        health.healthUpdated += OnHealthUpdated;
        OnHealthUpdated();
    }

    private void OnHealthUpdated ()
    {
        healthBar.fillAmount = health.normalized();
    }
}