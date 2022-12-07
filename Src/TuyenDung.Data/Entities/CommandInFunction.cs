using System;
using System.Collections.Generic;

namespace TuyenDung.Data.Entities
{
    public partial class CommandInFunction
    {
        public string CommandId { get; set; } = null!;
        public string FunctionId { get; set; } = null!;
    }
}
