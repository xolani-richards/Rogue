using UnityEngine;
using UnityEngine.UI;
using ROGUE.Characters;

public class HeroStaminaBar : MonoBehaviour 
{
    [SerializeField] Image staminaBar;
    [SerializeField] Hero hero;
    Stamina stamina;
    

    private void Start () {
        hero = Hero.instance;
        stamina = hero.stamina;
        stamina.staminaUpdated += OnStaminaUpdated;
    }

    private void OnStaminaUpdated ()
    {
        staminaBar.fillAmount = stamina.normalized();
    }
}