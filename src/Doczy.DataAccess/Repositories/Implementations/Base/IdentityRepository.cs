using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Interfaces.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Doczy.DataAccess.Repositories.Implementations.Base
{
    public class IdentityRepository<T> : IIdentityRepository<T> where T : BaseAppUser
    {
        private readonly DoczyContext _context;

        public IdentityRepository(DoczyContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<T> GetByIdAsync(Guid id)     
            => await Table.FindAsync(id);
          

        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
