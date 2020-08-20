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
}
