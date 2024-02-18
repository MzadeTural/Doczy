using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Core.Entities.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
