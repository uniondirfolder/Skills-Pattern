using System;
using System.Collections.Generic;
using System.Text;

namespace Stateless
{
    public partial class AStateMachine<TState, TTrigger> 
    {
        /// <summary>
        /// Describes a state transition.
        /// Описывает переход между состояниями.
        /// </summary>
        public class Transition // переход
        {
            readonly TState _source;
            readonly TState _destination;
            readonly TTrigger _trigger;

            /// <summary>
            /// Construct a transition.
            /// </summary>
            /// <param name="source">The state transitioned from. Состояние перешло с.</param>
            /// <param name="destination">The state transitioned to. Состояние перешло в.</param>
            /// <param name="trigger">The trigger that caused the transition. Триггер, вызвавший переход.</param>
            public Transition(TState source, TState destination, TTrigger trigger) 
            {
                _source = source;
                _destination = destination;
                _trigger = trigger;
            }

            /// <summary>
            /// The state transitioned from.
            /// </summary>
            public TState Source { get { return _source; } }

            /// <summary>
            /// The state transitioned to.
            /// </summary>
            public TState Destination { get { return _destination; } }

            /// <summary>
            /// The trigger that caused the transition.
            /// </summary>
            public TTrigger Trigger { get { return _trigger; } }

            /// <summary>
            /// True if the transition is a re-entry, i.e. the identity transition.
            /// Истинно, если переход является повторным входом, то есть переходом идентичности.
            /// </summary>
            public bool IsReentry { get { return Source.Equals(Destination); } }
        }

    }
    
}
