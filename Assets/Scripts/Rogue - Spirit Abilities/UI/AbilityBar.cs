using System.Collections.Generic;
using ROGUE.Abilities;
using ROGUE.Characters;
using UnityEngine;

public class AbilityBar: MonoBehaviour
{
    [SerializeField] AbilityIcon abilityIconPrefab;
    [SerializeField] int slots;
    [SerializeField] List<AbilityIcon> icons = new ();
    AbilityCaster caster;
    AbilityItem[] abilities;
    AbilityIcon selectedItem;

    Dictionary<AbilityItem, AbilityIcon> index = new ();

    void Start()
    {
        caster = Hero.instance.GetComponent<AbilityCaster>();
        abilities = caster.GetAbilityItems();
        slots = caster.abilitySlotLimit;

        for (int i = 0; i < slots; i++)
        {
            AbilityIcon icon = Instantiate (abilityIconPrefab, transform);
            icons.Add (icon);
            if(abilities.Length <= i) continue;
            icon.Bind(abilities[i]);
            index.Add(abilities[i], icon);
        }

        caster.onSelectionUpdated += OnSelectionUpdated;
        OnSelectionUpdated();
    }

    void OnSelectionUpdated ()
    {
        AbilityItem selected = caster.GetCurrentAbility();
        if(index.TryGetValue(selected, out AbilityIcon newIcon) && newIcon != selectedItem)
        {
            if(selectedItem != null) selectedItem.OnUnselected();
            selectedItem = newIcon;
            selectedItem.OnSelected();
        }
        
    }
}