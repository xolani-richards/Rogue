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
    }
}