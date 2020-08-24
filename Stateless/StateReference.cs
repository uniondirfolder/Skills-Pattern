using System;
using System.Collections.Generic;
using System.Text;

namespace Stateless
{
    public partial class AStateMachine<TState, TTrigger>
    {
        internal class StateReference
        {
            public TState State { get; set; }
        }
    }
    
}
