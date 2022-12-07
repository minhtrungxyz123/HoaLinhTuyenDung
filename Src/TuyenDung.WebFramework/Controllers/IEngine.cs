using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuyenDung.WebFramework.Controllers
{
    public interface IEngine
    {
        T Resolve<T>(string name = null) where T : class;
    }
}
