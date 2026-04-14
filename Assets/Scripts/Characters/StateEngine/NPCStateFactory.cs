using UnityEngine;

[CreateAssetMenu(fileName = "NPCStateFactory", menuName = "ROGUE/States/Factories/NPC")]
public class NPCStateFactory : StateFactory
{
    public override BaseStateFactory Init(StateEngine engine) => new NPCBaseFactory(engine);
}

public class NPCBaseFactory : BaseStateFactory
{
    public NPCBaseFactory(StateEngine engine) : base(engine)
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