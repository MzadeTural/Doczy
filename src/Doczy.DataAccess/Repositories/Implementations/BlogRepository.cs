using Doczy.Core.Entities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations.Base;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.DataAccess.Repositories.Implementations
{
	public class BlogRepository:Repository<Blog>,IBlogRepository
	{
		public BlogRepository(DoczyContext context):base(context)
		{
		}
	}
}

