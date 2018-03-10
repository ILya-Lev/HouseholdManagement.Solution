namespace DataAccess.Models.Local.Dto
{
	public class UtilityPriceDto
	{
		public virtual int Id { get; set; }
		public virtual double From { get; }
		public virtual double? To { get; set; }
		public virtual decimal Price { get; }
	}
}