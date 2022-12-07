using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuyenDung.WebFramework.Controllers
{
    public class BaseSingleton
    {
        static BaseSingleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Dictionary of type to singleton instances.
        /// </summary>
        public static IDictionary<Type, object> AllSingletons { get; }
    }
}
