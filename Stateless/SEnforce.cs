using System;

namespace Stateless
{
    static class SEnforce //обеспечивать соблюдение
    {
        public static T ArgumentNotNull<T>(T argument, string description)
            where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(description);
            return argument;
        }
    }
}
