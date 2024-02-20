using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Blog:BaseSectionEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string  Content{ get; set; }
        public string ImageUrl { get; set; }
        public DoctorAppUser Doctor{ get; set; }
        public  Guid DoctorId { get; set; }
    }
}
