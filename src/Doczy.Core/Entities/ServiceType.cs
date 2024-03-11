using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class ServiceType:BaseAuditableEntity
    {
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public List<Service> Services { get; set; }
    }
}
