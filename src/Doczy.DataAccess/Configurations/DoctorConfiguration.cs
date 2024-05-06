using Doczy.Core.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doczy.DataAccess.Configurations
{
    public class DoctorConfiguration: IEntityTypeConfiguration<DoctorAppUser>
    {
        public void Configure(EntityTypeBuilder<DoctorAppUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(30).IsRequired(true); 
            builder.Property(x => x.LastName).HasMaxLength(30).IsRequired(true); 
            builder.Property(x => x.AboutDoctor).HasMaxLength(500); 
        }
    }
}
