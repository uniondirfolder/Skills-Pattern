using System;
using System.Collections.Generic;
using System.Text;

namespace Stateless
{
    static class SParameterConversion //Преобразование параметров
    {
        public static object Unpack(object[] args, Type argType, int index) 
        {
            SEnforce.ArgumentNotNull(args, "args");

            if (args.Length <= index)
                throw new ArgumentException(
                    string.Format(RParameterConversionResources.ArgOfTypeRequiredInPosition, argType, index));

            var arg = args[index];

            if (arg != null && !argType.IsAssignableFrom(arg.GetType()))
                throw new ArgumentException(
                    string.Format(RParameterConversionResources.WrongArgType, index, arg.GetType(), argType));

            return arg;
        }
        public static TArg Unpack<TArg>(object[] args, int index) 
        {
            return (TArg)Unpack(args, typeof(TArg), index);
        }
        public static void Validate(object[] args, Type[] expected) 
        {
            if (args.Length > expected.Length)
                throw new ArgumentException(
                    string.Format(RParameterConversionResources.TooManyParameters, expected.Length, args.Length));

            for (int i = 0; i < expected.Length; ++i)
            {
                Unpack(args, expected[i], i);
            }
        }
    }
}
