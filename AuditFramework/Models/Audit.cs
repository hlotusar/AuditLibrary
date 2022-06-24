using System;
using System.Collections.Generic;

namespace AuditFramework.Models
{
    public partial class Audit
    {
        public int Auditid { get; set; }
        public string Actionname { get; set; } = null!;
        public string Tablename { get; set; } = null!;
        public string Propertyname { get; set; } = null!;
        public string? Oldvalue { get; set; }
        public DateTime Updatedat { get; set; }
    }
}
