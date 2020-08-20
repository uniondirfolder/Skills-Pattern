using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

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

            public void ValidateParameters(object[] args) 
            {
                SEnforce.ArgumentNotNull(args, "args");
                Parame
            }
        }
    }
    
}
