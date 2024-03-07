using Doczy.Core.Entities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.DataAccess.Repositories.Implementations
{
    public class DoctorRatingRepository : Repository<DoctorRating>, IDoctorRatingRepository
    {
        public DoctorRatingRepository(DoczyContext context) : base(context)
        {
        }
    }
}
