namespace ROGUE.Abilities
{
    public interface IEffectTarget: IGameObject
    {
        void ApplyEffect(EffectData effect, float value);
    }   
}