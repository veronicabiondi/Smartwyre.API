using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data
{
    public static class TypeHelper
    {
        public static IEnumerable<Type> GetTypes(Func<Type, bool> expression)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(expression).ToList();
        }
    }
}