using System;

namespace Stateless
{

    partial class AStateMachine<TState, TTriger>
    {
        /// <summary>
        /// Associates configured parameters with an underlying trigger value.
        /// Связывает настроенные параметры с нижележащим значением триггера.
        /// </summary>
        public abstract class ATriggerWithParameters
        {
            readonly TTriger _underlyingTrigger;
            readonly Type[] _argumentTypes;     //Представляет объявления типов для классов, интерфейсов, массивов, значений, перечислений параметров, определений универсальных типов и открытых или закрытых сконструированных универсальных типов.
                                                //Type является корнем функции System.Reflection и является основным способом доступа к метаданным. Члены Type используются для получения сведений об объявлении типа, о членах типа (таких как конструкторы, методы, поля, свойства и события класса), а также о модуле и сборке, в которой развернут класс.
            /// <summary>
            /// Create a configured trigger.
            /// Создает настроенный триггер.
            /// </summary>
            /// <param name="underlyingTrigger">Trigger represented by this trigger configuration. Триггер, представленный этой конфигурацией триггера.</param>
            /// <param name="argumentTypes">The argument types expected by the trigger. Типы аргументов, ожидаемые триггером.</param>
            public ATriggerWithParameters(TTriger underlyingTrigger, params Type[] argumentTypes) 
            {
                SEnforce.ArgumentNotNull(argumentTypes, "argumentTypes");

                _underlyingTrigger = underlyingTrigger;
                _argumentTypes = argumentTypes;
            }

            /// <summary>
            /// Gets the underlying trigger value that has been configured. Получает настроенное базовое значение триггера.
            /// </summary>
            public TTriger Triger { get { return _underlyingTrigger; } }

            /// <summary>
            /// Ensure that the supplied arguments are compatible with those configured for this triger.
            /// Убедитесь, что предоставленные аргументы совместимы с аргументами, настроенными для этого триггера.
            /// </summary>
            /// <param name="args"></param>
            public void ValidateParameters(object[] args) 
            {
                SEnforce.ArgumentNotNull(args, "args");

                SParameterConversion.Validate(args, _argumentTypes);
            }
        }

        /// <summary>
        /// A configured trigger with one required argument. Настроенный триггер с одним обязательным аргументом.
        /// </summary>
        /// <typeparam name="TArg0"></typeparam>
        public class ATriggerWithParameters<TArg0> : ATriggerWithParameters
        {
            /// <summary>
            /// Create a configured trigger.
            /// </summary>
            /// <param name="underlyingTrigger">Trigger represented by this trigger configuration.</param>
            public ATriggerWithParameters(TTriger underlyingTrigger)
                : base(underlyingTrigger, typeof(TArg0)) { }
        }

        /// <summary>
        /// A configured trigger with two required arguments.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first argument.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument.</typeparam>
        public class ATriggerWithParameters<TArg0, TArg1> : ATriggerWithParameters
        {
            /// <summary>
            /// Create a configured trigger.
            /// </summary>
            /// <param name="underlyingTrigger"></param>
            public ATriggerWithParameters(TTriger underlyingTrigger)
                : base(underlyingTrigger, typeof(TArg0), typeof(TArg1)) 
            { }
        }

        /// <summary>
        /// A configured trigger with three required arguments.
        /// </summary>
        /// <typeparam name="TArg0">The type of the first argument.</typeparam>
        /// <typeparam name="TArg1">The type of the second argument.</typeparam>
        /// <typeparam name="TArg2">The type of the third argument.</typeparam>
        public class ATriggerWithParameters<TArg0, TArg1, TArg2> : ATriggerWithParameters 
        {
            /// <summary>
            /// Create a configured trigger.
            /// </summary>
            /// <param name="underlyingTrigger">Trigger represented by this trigger configuration.</param>
            public ATriggerWithParameters(TTriger underlyingTrigger)
                : base(underlyingTrigger, typeof(TArg0), typeof(TArg1), typeof(TArg2)) 
            { }
        }
    }
    
}
