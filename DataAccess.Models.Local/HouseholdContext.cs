using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using DataAccess.Models.Local.Contract;
using DataAccess.Models.Local.Dto;
using Model.UtilityPayments;
using NLog;

namespace DataAccess.Models.Local
{
	public class HouseholdContext : DbContext, ITariffContext
	{
		public HouseholdContext(string connectionString) : base(connectionString)
		{
			this.Database.Log = message => LogManager.GetCurrentClassLogger().Log(LogLevel.Info, message);
		}

		public DbSet<TariffDto> Tariffs { get; set; }

		IQueryable<TariffDto> ITariffContext.Tariffs => Tariffs;
	}
}
