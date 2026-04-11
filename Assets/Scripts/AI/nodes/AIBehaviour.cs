public class AIBehaviour
{
    public Selector behaviour;

    void Awake ()
    {
        behaviour = new Selector();

        Sequencer InCombat = new Sequencer("In Combat");
        Sequencer InDanger = new Sequencer("In Danger");
        Sequencer HasTask = new Sequencer("Complete Task");
        Sequencer Idle = new Sequencer("Idle");
    }
}