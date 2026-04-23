using System;
using UnityEngine;

[Serializable]
public class Stats {
    [SerializeField] float meleeAttack;
    [SerializeField] float meleeDefense;
    [SerializeField] float magicAttack;
    [SerializeField] float magicDefense;
    [SerializeField] float staminaRecovery;
    [SerializeField] float maxHealth;
    [SerializeField] float maxStamina;
    
    readonly StatsMediator mediator;
    readonly BaseStats baseStats;
    
    public StatsMediator Mediator => mediator;

    public float MaxHealth => GetStat(StatType.MaxHealth, baseStats.maxHealth, out maxHealth);
    public float MaxStamina => GetStat(StatType.MaxStamina, baseStats.maxStamina, out maxStamina);
    public float MeleeAttack => GetStat(StatType.MeleeAttack, baseStats.meleeAttack, out meleeAttack);
    public float MeleeDefense => GetStat(StatType.MeleeDefense, baseStats.meleeDefense, out meleeDefense);
    public float MagicAttack => GetStat(StatType.MagicAttack, baseStats.magicAttack, out magicAttack);
    public float MagicDefense => GetStat(StatType.MagicDefense, baseStats.magicDefense, out magicDefense);
    public float StaminaRecoveryRate => GetStat(StatType.StaminaRecovery, baseStats.staminaRecovery, out staminaRecovery);
    
    public Stats(StatsMediator mediator, BaseStats baseStats) {
        this.mediator = mediator;
        this.baseStats = baseStats;
    }

    private float GetStat(StatType type, float value, out float result)
    {
        var q = new Query(type, value);
        mediator.PerformQuery(this, q);
        result = q.Value;
        return result;
    }
    
    public override string ToString() => $"DMG: {MeleeAttack}, DEF: {MeleeDefense}, MDMG: {MagicAttack}, MDEF: {magicDefense}, STR: {StaminaRecoveryRate}";
}