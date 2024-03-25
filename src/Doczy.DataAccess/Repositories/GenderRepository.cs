using Doczy.Core.Entities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.DataAccess.Repositories
{
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(DoczyContext context) : base(context)
        {
        }
    }
}
