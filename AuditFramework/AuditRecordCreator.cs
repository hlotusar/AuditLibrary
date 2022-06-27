using AuditFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication2.Attributes;

namespace AuditFramework
{
    public class AuditRecordCreator
    {
        private DbContext _dbContext;

        public AuditRecordCreator(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        AuditdbContext _auditDbContext = new AuditdbContext();

        public void CreateAuditRecords()
        {
            if (_dbContext.ChangeTracker.HasChanges())
            {
                _dbContext.ChangeTracker.DetectChanges();
                var entries = _dbContext.ChangeTracker.Entries().AsEnumerable();
                List<Audit> auditlist = new List<Audit>();
                foreach (var entry in entries)
                {
                    var entityType = entry.Entity.GetType();
                    if (IsAuditable(entityType))
                    {
                        foreach (var property in entry.Properties)
                        {
                            if (entityType.GetProperty(property.Metadata.Name).GetCustomAttributes(typeof(AuditableAttribute), false).Any())
                            {
                                {
                                    Audit audit = GetAuditRecord(entry, property);
                                    auditlist.Add(audit);
                                }

                            }
                        }
                    }
                }

                if (auditlist?.Count > 0)
                {
                    _auditDbContext.Audits.AddRange(auditlist);
                    _auditDbContext.SaveChanges();
                }
            }

        }

        private Audit GetAuditRecord(EntityEntry entry,PropertyEntry property)
        {
            Audit audit = new Audit();
            audit.AuditId=Guid.NewGuid();
            audit.EntityId = GetPrimaryKey(entry);
            audit.EntityType = entry.Entity.GetType().Name;
            audit.ActionName = entry.State.ToString();
            audit.PropertyName = property.Metadata.Name;
            audit.Oldvalue = (entry.State == EntityState.Added) ? "N/A" : property.OriginalValue?.ToString();
            audit.NewValue = (entry.State == EntityState.Deleted) ? "N/A" : property.CurrentValue?.ToString();
            audit.UpdatedAt = DateTime.Now;
            audit.TransactionId = _dbContext.ContextId.InstanceId.ToString();
            return audit;
        }

        private string GetPrimaryKey(EntityEntry entry)
        {
            string pk = string.Empty;
            var type = entry.Entity.GetType();
            if (!entry.IsKeySet) return "N/A";

            foreach (var property in _dbContext.Model.FindEntityType(type).FindPrimaryKey().Properties.ToList())

            {
                var propertyValue = property.PropertyInfo.GetValue(entry.Entity);
                pk += string.Format("{0}={1};", property.Name, entry.Property(property.Name).CurrentValue);

            }

            return pk;
        }

        private bool IsAuditable(Type type)
        {
            return type.GetCustomAttributes(typeof(AuditableAttribute), false).Any();
        }
    }
}
