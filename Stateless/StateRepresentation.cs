using System;
using System.Collections.Generic;
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

            readonly ICollection<Action<Transiti>>
        }
    }
    
}
