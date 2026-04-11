public interface ILeafStrategy
{
    void OnEnter();
    void OnLeave();
    Node.Status OnProcess(float deltaTime);
}