using UnityEngine;
using UnityEngine.UI;
using ROGUE.Characters;

public class HeroHealthBar : MonoBehaviour 
{
    [SerializeField] Image healthBar;
    [SerializeField] Hero hero;
    Health health;
    

    private void Start () {
        hero = Hero.instance;
        health = hero.health;
        health.healthUpdated += OnHealthUpdated;
    }

    private void OnHealthUpdated ()
    {
        healthBar.fillAmount = health.normalized();
    }
}