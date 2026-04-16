using ROGUE.Characters;
public interface IActor: IGameObject
{
    public Character character { get; }
    public void OnAccept(IVisitor visitor);
}