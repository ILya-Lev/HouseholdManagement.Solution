using System.Collections.Generic;

namespace DataAccess.Models.Local.Dto
{
	public class TariffDto
	{
		public virtual int Id { get; set; }
		public virtual IList<UtilityPriceDto> Prices { get; set; }
	}
}