using System;
using System.Diagnostics;
using Autofac;
//using PostSharp.Aspects.Internals;
using static System.Console;

namespace S0002_DN_Singleton
{
    public class Foo
    {
        public EventBroker Broker;

        public Foo(EventBroker broker)
        {
            Broker = broker ?? throw new ArgumentNullException(paramName: nameof(broker));
        }
    }

    public class EventBroker
    {
    }
}
