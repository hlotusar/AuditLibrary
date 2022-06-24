using AuditFramework.Models;
using Microsoft.EntityFrameworkCore;
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

        AuditdbContext _auditDbContext=new AuditdbContext();

        public void CreateAuditRecords()
        {
            if(_dbContext.ChangeTracker.HasChanges())
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

        private Audit GetAuditRecord(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry, Microsoft.EntityFrameworkCore.ChangeTracking.PropertyEntry property)
        {
            Audit audit = new Audit();
            audit.Updatedat = DateTime.Now;
            audit.Actionname = entry.State.ToString();
            audit.Tablename = entry.Entity.GetType().Name;
            audit.Propertyname = property.Metadata.Name;
            audit.Oldvalue = (entry.State == EntityState.Added) ? string.Empty : property.OriginalValue?.ToString();
            return audit;
        }

        private bool IsAuditable(Type type)
        {
            return type.GetCustomAttributes(typeof(AuditableAttribute), false).Any();
        }
    }
}
