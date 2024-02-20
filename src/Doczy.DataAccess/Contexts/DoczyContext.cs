using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Doczy.DataAccess.Contexts
{
    public class DoczyContext :IdentityDbContext<BaseAppUser, IdentityRole<Guid>, Guid>
    {
        public DoczyContext(DbContextOptions<DoczyContext> options) : base(options) { }
    
    }
}
