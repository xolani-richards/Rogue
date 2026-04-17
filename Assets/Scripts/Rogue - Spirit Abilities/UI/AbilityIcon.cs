using ROGUE.Abilities;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Image icon;
    [SerializeField] Image countDownBar;
    [SerializeField] Color selectedColor;
    [SerializeField] Color notSelectedColor;
    AbilityItem abilityItem;

    void Awake()
    {
        countDownBar.fillAmount = 0;
        icon.gameObject.SetActive(false);
        OnUnselected();
    }

    public void Bind (AbilityItem item)
    {
        abilityItem = item;
        abilityItem.onUpdate += OnUpdate;
        
        icon.gameObject.SetActive(true);
        if(item.ability.icon != null) icon.sprite = item.ability.icon;
        
    }

    public void OnSelected () {
        background.color = selectedColor;
        transform.localScale = Vector3.one;
    }
    public void OnUnselected () {
        background.color = notSelectedColor;
        transform.localScale = Vector3.one * 0.8f;
    }

    private void OnDestroy()
    {
        if(abilityItem != null) abilityItem.onUpdate -= OnUpdate;
    }

    private void OnUpdate()
    {
        countDownBar.fillAmount = abilityItem.timer / abilityItem.cooldown;
    }

    //TODO: add tweening to make icon animate when charged
    private void OnReady()
    {
        
    }
}
