using R3;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorEvents
    {
        public ReactiveProperty<bool> CanFire { get; } = new();
    }
}
