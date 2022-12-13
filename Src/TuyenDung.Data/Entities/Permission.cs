using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class Permission
    {
        public Permission(string functionId, string roleId, string commandId)
        {
            FunctionId = functionId;
            RoleId = roleId;
            CommandId = commandId;
        }
        public string FunctionId { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public string CommandId { get; set; } = null!;
    }
}
