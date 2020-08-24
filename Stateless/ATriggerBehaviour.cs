using System;
using System.Collections.Generic;
using System.Text;

namespace Stateless
{

    public partial class AStateMachine<TState, TTrigger> 
    {
        internal abstract class ATriggerBehaviour
        {
            readonly TTrigger _trigger;
            readonly Func<bool> _guard;

            protected ATriggerBehaviour(TTrigger trigger, Func<bool> guard) 
            {
                _trigger = trigger;
                _guard = guard;
            }

            public TTrigger Trigger { get { return _trigger; } }

            public bool IsGuardConditionMet
            {
                get 
                {
                    return _guard();
                }
            }

            public abstract bool ResultsInTransitionFrom(TState source, object[] args, out TState distination);
        }
    }
    
}
