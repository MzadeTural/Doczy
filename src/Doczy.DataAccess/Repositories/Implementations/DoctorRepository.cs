using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.DataAccess.Repositories.Implementations
{
    public class DoctorRepository : IdentityRepository<DoctorAppUser>, IDoctorRepository
    {
        public DoctorRepository(DoczyContext context) : base(context)
        {
        }
    }
}
