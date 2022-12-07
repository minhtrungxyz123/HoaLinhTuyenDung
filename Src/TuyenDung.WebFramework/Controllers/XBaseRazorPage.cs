using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuyenDung.Base;

namespace TuyenDung.WebFramework.Controllers
{
    // TODO-XBase: UserBase, MobileDevice
    /// <summary>
    /// Web view page
    /// </summary>
    /// <typeparam name="TModel">Model</typeparam>
    public abstract class XBaseRazorPage<TModel> : Microsoft.AspNetCore.Mvc.Razor.RazorPage<TModel>
    {
        private string _clientFormId;

        /// <summary>
        /// Unique client form Id per user request.
        /// </summary>
        public string ClientFormId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_clientFormId))
                    return _clientFormId;

                var clientFormId = Context.Request.GetValue("ClientFormId");
                _clientFormId = !clientFormId.IsEmpty() ? clientFormId : DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

                return _clientFormId;
            }
            set => _clientFormId = value;
        }

        protected XBaseRazorPage()
        {
        }

    }

    /// <summary>
    /// Admin web view page
    /// </summary>
    /// <typeparam name="TModel">Model</typeparam>
    public abstract class XBaseAdminRazorPage<TModel> : XBaseRazorPage<TModel>
    {
        private AdminAreaSettings _adminAreaSettings;
        public AdminAreaSettings AdminAreaSettings => _adminAreaSettings;

        protected XBaseAdminRazorPage()
        {
            _adminAreaSettings = EngineContext.Current.Resolve<AdminAreaSettings>();
        }
    }

    /// <summary>
    /// Web view page
    /// </summary>
    public abstract class XBaseRazorPage : XBaseRazorPage<dynamic>
    {
    }
}
