using ROGUE.Abilities;
using ROGUE.Characters;
using UnityEngine;

public class AbilityTest: MonoBehaviour
{
    AbilityIcon abilityIcon;
    void Start()
    {
        AbilityCaster caster = Hero.instance.GetComponent<AbilityCaster>();
        AbilityItem[] items = caster.GetAbilityItems();
        abilityIcon = GetComponent<AbilityIcon>();
        abilityIcon.Bind(items[0]);
    }
}