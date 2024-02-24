using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class ServiceType:BaseSectionEntity
    {
        public string IconUrl { get; set; }
        public string Name { get; set; }
        public List<Service> Services { get; set; }
    }
}
