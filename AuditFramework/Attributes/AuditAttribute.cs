
namespace WebApplication2.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple =true)]
    public class AuditableAttribute : Attribute
    {
    }
}
