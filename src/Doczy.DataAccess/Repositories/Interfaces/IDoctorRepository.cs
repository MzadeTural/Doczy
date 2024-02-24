using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.DataAccess.Repositories.Interfaces
{
    public interface IDoctorRepository:IIdentityRepository<DoctorAppUser>
    {
    }
}
