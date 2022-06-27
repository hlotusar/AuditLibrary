using System;
using System.Collections.Generic;

namespace AuditFramework.Models
{
    public partial class Audit
    {
        public Guid AuditId { get; set; }
        public string EntityId { get; set; } = null!;
        public string EntityType { get; set; } = null!;
        public string ActionName { get; set; } = null!;
        public string PropertyName { get; set; } = null!;
        public string? Oldvalue { get; set; }
        public string? NewValue { get; set; }
        public string? ModuleName { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string TransactionId { get; set; } = null!;
        public string? User { get; set; }
    }
}
