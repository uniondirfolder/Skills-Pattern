using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stateless
{
    public partial class AStateMachine<TState, TTrigger> 
    {
        internal class StateRepresentation
        {
            readonly TState _state;
            readonly IDictionary<TTrigger, ICollection<ATriggerBehaviour>> _triggerBehaviours =
                new Dictionary<TTrigger, ICollection<ATriggerBehaviour>>();

            readonly ICollection<Action<Transition, object[]>> _entryActions = new List<Action<Transition, object[]>>();
            readonly ICollection<Action<Transition>> _exitActions = new List<Action<Transition>>();

            StateRepresentation _superstate; //null

            readonly ICollection<StateRepresentation> _substates = new List<StateRepresentation>();

            public StateRepresentation(TState state) 
            {
                _state = state;
            }

            bool TryFindLocalHandler(TTrigger trigger, out ATriggerBehaviour handler) 
            {
                ICollection<ATriggerBehaviour> possible;
                if (!_triggerBehaviours.TryGetValue(trigger, out possible))
                {
                    handler = null;
                    return false;
                }

                var actual = possible.Where(at => at.IsGuardConditionMet).ToArray();

                if (actual.Count() > 1)
                    throw new InvalidOperationException(
                        string.Format(RStateRepresentationResources.))
            }
            public bool TryFindHandler(TTrigger trigger, out ATriggerBehaviour handler) 
            {
                return(Tr)
            }

            public bool CanHandle(TTrigger trigger) 
            {
                ATriggerBehaviour unused;
                return TryFi
            }
        }
    }
    
}
