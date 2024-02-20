using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Gender:BaseEntity
    {
        public string? Name { get; set; }
        public List<BaseAppUser>? Users{ get; set; }
    }
}
