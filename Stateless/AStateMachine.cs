using System;
using System.Collections.Generic;
using System.Text;

namespace Stateless
{
    /// <summary>
    /// Базовый класс для всех конечных автоматов12345
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

    /// <summary>
    /// Models behaviour as transitions between a finite set of states. Моделирует поведение как переходы между конечным набором состояний.
    /// </summary>
    /// <typeparam name="TState">The type used to represent the states. Тип, используемый для представления состояний.</typeparam>
    /// <typeparam name="TTrigger">The type used to represent the triggers that cause state transitions. Тип, используемый для представления триггеров, вызывающих переходы между состояниями.</typeparam>
    public partial class AStateMachine<TState, TTrigger> : AStateMachine
    {
        readonly IDictionary<TState, StateRepresentation> _stateConfiguration = new Dictionary<TState, StateRepresentation>();
        readonly IDictionary<TTrigger, ATriggerWithParameters> _triggerConfiguration = new Dictionary<TTrigger, ATriggerWithParameters>();

        readonly Func<TState> _stateAccessor;
        readonly Action<TState> _stateMutator;
        Action<TState, TTrigger> _unhandledTriggerAction;

        /// <summary>
        /// Construct a state machine with external state storage. Создайте конечный автомат с внешним хранилищем состояний.
        /// </summary>
        /// <param name="stateAccessor">A function that will be called to read the current state value. Функция, которая будет вызываться для чтения текущего значения состояния.</param>
        /// <param name="stateMutator">An action that will be called to write new state values. Действие, которое будет вызываться для записи новых значений состояния.</param>
        public AStateMachine(Func<TState> stateAccessor, Action<TState> stateMutator) 
        {
            _stateAccessor = SEnforce.ArgumentNotNull(stateAccessor, "stateAccessor");
            _stateMutator = SEnforce.ArgumentNotNull(stateMutator, "stateMutator");
        }

        /// <summary>
        /// Construct a state machine. Построение конечного автомата.
        /// </summary>
        /// <param name="initialState">The initial state. Исходное состояние.</param>
        public AStateMachine(TState initialState) 
        {
            var reference = new StateReference { State = initialState};
            _stateAccessor = () => reference.State;
            _stateMutator = s => reference.State = s;
        }

        /// <summary>
        /// The current state.
        /// </summary>
        public TState State 
        {
            get { return _stateAccessor(); }
            private set { _stateMutator(value); }
        }

        public IEnumerable<TTrigger> PermittedTriggers 
        {
            get { return Curr}
        }
    }
}
