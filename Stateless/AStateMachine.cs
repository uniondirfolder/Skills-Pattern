using System;
using System.Collections.Generic;
using System.Text;

namespace Stateless
{
    /// <summary>
    /// Базовый класс для всех конечных автоматов
    /// </summary>
    public abstract class AStateMachine
    {
        readonly Guid _id; //Представляет глобальный уникальный идентификатор (GUID).
        
        /// <summary>
        /// Инициализирует id
        /// </summary>
        protected AStateMachine() 
        {
            _id = Guid.NewGuid();
        }

        /// <summary>
        ///Уникальный идентификатор этого конечного автомата.
        /// </summary>
        public Guid Id 
        {
            get { return this._id; }
        }
    }
    public partial class AStateMachine<TState, TTrigger> : AStateMachine
    {
        readonly IDictionary<TState,StateRepresentation>_

        readonly Func<TState> _stateAccessor;
        readonly Action<TState> _stateMutator;

        public AStateMachine(Func<TState> stateAccessor, Action<TState> stateMutator) 
        {
            _stateAccessor = SEnforce.ArgumentNotNull(stateAccessor, "stateAccessor");
            _stateMutator = SEnforce.ArgumentNotNull(stateMutator, "stateMutator");
        }
    }
}
