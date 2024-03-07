using Doczy.Core.Entities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.DataAccess.Repositories.Implementations
{
    public class FavoriteDoctorRepository : Repository<FavoriteDoctor>, IFavoriteDoctorRepository
    {
        public FavoriteDoctorRepository(DoczyContext context) : base(context)
        {
        }
    }
}
