using Unity.VisualScripting;

namespace SurvShoo.ActorControllers.VisualScripts
{
    [UnitTitle("OnGameEnter")]
    [UnitCategory("Events\\SurvShoo/ActorControllers")]
    public class OnGameEnter : EventUnit<Actor>
    {
        [DoNotSerialize]
        public ValueOutput owner { get; private set; }

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(Messages.OnGameEnter);
        }

        protected override void Definition()
        {
            base.Definition();
            owner = ValueOutput<Actor>(nameof(owner));
        }

        protected override void AssignArguments(Flow flow, Actor owner)
        {
            flow.SetValue(this.owner, owner);
        }
    }
}
