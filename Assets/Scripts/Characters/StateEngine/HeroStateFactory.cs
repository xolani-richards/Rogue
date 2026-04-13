using UnityEngine;

[CreateAssetMenu(fileName = "HeroStateFactory", menuName = "ROGUE/States/Factories/Hero")]
public class HeroStateFactory : StateFactory
{
    public override BaseStateFactory Init(StateEngine engine) => new HeroBaseFactory(engine);
}

public class HeroBaseFactory : BaseStateFactory
{
    public HeroBaseFactory(StateEngine engine) : base(engine)
    {}
    protected override void CreateStates()
    {
        states = new();
        states.Add(Statename.Idle, new IdleState(stateEngine));
        states.Add(Statename.Moving, new MovingState(stateEngine));
        states.Add(Statename.TakingDamage, new TakingDamageState(stateEngine));
        states.Add(Statename.Dead, new DeadState(stateEngine));
        states.Add(Statename.Blocking, new BlockingState(stateEngine));
        states.Add(Statename.Attacking, new AttackState(stateEngine));
    }
}