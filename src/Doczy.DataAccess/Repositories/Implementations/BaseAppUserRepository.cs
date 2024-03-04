using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;
using Doczy.DataAccess.Repositories.Interfaces.Base;

namespace Doczy.DataAccess.Repositories.Implementations
{
    public class BaseAppUserRepository : IdentityRepository<BaseAppUser>, IBaseAppUserRepository
    {
        public BaseAppUserRepository(DoczyContext context) : base(context)
        {
        }
    }
}
