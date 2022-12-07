using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TuyenDung.WebFramework.Controllers
{
    public class XBaseEngine : IEngine
    {
        public T Resolve<T>(string name = null) where T : class
        {
            return (T)Resolve(typeof(T), name);
        }

        public object Resolve(Type type, string name = null)
        {
            var sp = GetServiceProvider();
            if (sp == null)
                return null;

            // Custom-DI
            if (!string.IsNullOrEmpty(name))
            {
                var root = sp.GetAutofacRoot();
                return root.ResolveNamed(name, type);
            }

            return sp.GetService(type);
        }

        protected IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider == null)
                return null;
            var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        private IServiceProvider _serviceProvider { get; set; }
        public virtual IServiceProvider ServiceProvider => _serviceProvider;
    }
}