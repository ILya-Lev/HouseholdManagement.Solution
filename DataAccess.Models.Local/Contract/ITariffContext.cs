using System.Linq;
using DataAccess.Models.Local.Dto;

namespace DataAccess.Models.Local.Contract
{
	public interface ITariffContext
	{
		IQueryable<TariffDto> Tariffs { get; }
	}
}