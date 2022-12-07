using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class Permission
    {
        public string FunctionId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string CommandId { get; set; } = null!;
    }
}
