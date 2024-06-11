using Unity.VisualScripting;

namespace SurvShoo.ActorControllers.VisualScripts
{
    [UnitTitle("OnGameEnter")]
    [UnitCategory("Events\\SurvShoo/ActorControllers")]
    public class OnGameEnter : EventUnit<Actor>
    {
        [DoNotSerialize]
        public ValueOutput result { get; private set; }

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(Messages.OnGameEnter);
        }

        protected override void Definition()
        {
            base.Definition();
            result = ValueOutput<Actor>(nameof(result));
        }

        protected override void AssignArguments(Flow flow, Actor data)
        {
            flow.SetValue(result, data);
        }
    }
}
