using R3;

namespace SurvShoo
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ActorEvents
    {
        public Subject<Unit> OnPoolRent { get; } = new();
        
        public Subject<Unit> OnPoolReturn { get; } = new();
        
        public ReactiveProperty<bool> CanFire { get; } = new();
    }
}
