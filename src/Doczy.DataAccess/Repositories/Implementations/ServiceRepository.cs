using Doczy.Core.Entities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.DataAccess.Repositories.Implementations
{
    public class ServiceRepository : Repository<Service>, IServicerepository
    {
        public ServiceRepository(DoczyContext context) : base(context)
        {
        }
    }
}
