using System.Runtime.CompilerServices;

namespace TuyenDung.ModelName.XBaseModel
{
    public class XBaseResourceDisplayName : System.ComponentModel.DisplayNameAttribute, IModelAttribute
    {
        private readonly string _callerPropertyName;

        public XBaseResourceDisplayName(string resourceKey, [CallerMemberName] string propertyName = null)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
            _callerPropertyName = propertyName;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                string value = ResourceKey;
                return value;
            }
        }

        public string Name => nameof(XBaseResourceDisplayName);
    }
}