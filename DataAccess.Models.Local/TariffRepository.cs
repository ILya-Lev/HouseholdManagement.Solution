using DataAccess.Models.Local.Contract;

namespace DataAccess.Models.Local
{
	public class TariffRepository
	{
		private readonly ITariffContext _context;

		public TariffRepository(ITariffContext context)
		{
			_context = context;
		}
	}
}