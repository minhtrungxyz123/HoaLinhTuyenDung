using System;
using System.Collections.Generic;
using System.Text;

namespace TuyenDung.Core.FormFile
{
    [Serializable]
    public class BaseEntity
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
