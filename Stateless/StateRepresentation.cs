using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                        string.Format(RStateRepresentationResources.MultipleTransitionsPermitted, trigger, _state));

                handler = actual.FirstOrDefault();
                return handler != null;
            }

            StateRepresentation _superstate; //null
            public StateRepresentation Superstate 
            {
                get { return _superstate; }
                set { _superstate = value; }
            }
            public bool TryFindHandler(TTrigger trigger, out ATriggerBehaviour handler) 
            {
                return (
                    TryFindLocalHandler(trigger, out handler) ||
                    (Superstate != null && Superstate.TryFindHandler(trigger, out handler))
                    );
            }
            public bool CanHandle(TTrigger trigger) 
            {
                ATriggerBehaviour unused;
                return TryFindHandler(trigger, out unused);
            }

            public void AddEntryAction(TTrigger trigger, Action<Transition, object[]> action) 
            {
                SEnforce.ArgumentNotNull(action, "action");
                _entryActions.Add((t, args) =>
                {
                    if (t.Trigger.Equals(trigger))
                        action(t, args);
                });
            }

            public void AddEntryAction(Action<Transition, object[]> action) 
            {
                _entryActions.Add(SEnforce.ArgumentNotNull(action, "action"));
            }
            public void AddExitAction(Action<Transition> action) 
            {
                _exitActions.Add(SEnforce.ArgumentNotNull(action, "action"));
            }

            void ExecuteEntryActions(Transition transition, object[] entryArgs) 
            {
                SEnforce.ArgumentNotNull(transition, "transtion");
                SEnforce.ArgumentNotNull(entryArgs, "entryArgs");
                foreach (var action in _entryActions)
                    action(transition, entryArgs);
            }
            public bool Includes(TState state)
            {
                return _state.Equals(state) || _substates.Any(s => s.Includes(state));
            }
            public void Enter(Transition transition, params object[] entryArgs) 
            {
                SEnforce.ArgumentNotNull(transition, "transtion");
                if (transition.IsReentry)
                {
                    ExecuteEntryActions(transition, entryArgs);
                }
                else if (!Includes(transition.Source)) 
                {
                    if (_superstate != null)
                        _superstate.Enter(transition, entryArgs);

                    ExecuteEntryActions(transition, entryArgs);
                }
            }

            void ExecuteExitActions(Transition transition)
            {
                SEnforce.ArgumentNotNull(transition, "transtion");
                foreach (var action in _exitActions)
                    action(transition);
            }
            public void Exit(Transition transition) 
            {
                SEnforce.ArgumentNotNull(transition, "transition");
                if (transition.IsReentry)
                {
                    ExecuteExitActions(transition);
                }
                else if (!Includes(transition.Destination)) 
                {
                    ExecuteExitActions(transition);
                    if (_superstate != null)
                        _superstate.Exit(transition);
                }
            }

            public void AddTriggerBehaviour(ATriggerBehaviour aTriggerBehaviour) 
            {
                ICollection<ATriggerBehaviour> allowed;
                if (_triggerBehaviours.TryGetValue(aTriggerBehaviour.Trigger, out allowed)) 
                {
                    allowed = new List<ATriggerBehaviour>();
                    _triggerBehaviours.Add(aTriggerBehaviour.Trigger, allowed);
                }
                allowed.Add(aTriggerBehaviour);
            }

            public TState UnderlyingState 
            {
                get { return _state; }
            }

            public void AddSubstate(StateRepresentation substate) 
            {
                SEnforce.ArgumentNotNull(substate, "substate");
                _substates.Add(substate);
            }

            public bool IsIncludeIn(TState state) 
            {
                return
                    _state.Equals(state) ||
                    (_superstate != null && _superstate.IsIncludeIn(state));
            }

            public IEnumerable<TTrigger> PermittedTriggers 
            {
                get 
                {
                    var result = _triggerBehaviours
                        .Where(t => t.Value.Any(a => a.IsGuardConditionMet))
                        .Select(t => t.Key);

                    if (Superstate != null)
                        result = result.Union(Superstate.PermittedTriggers);
                    return result.ToArray();
                }
            }
        }
    }
    
}
