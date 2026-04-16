using UnityEngine;

public interface IDamageable: IGameObject
{
    // bool DoDamage(GameObject caster, float baseValue);
    void TakeDamage(float amount);

}