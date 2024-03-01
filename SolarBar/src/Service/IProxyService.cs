using SolarBar.Model;
using SolarBar.Model.Dto;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SolarBar.Services
{
	public interface IProxyService
	{
		Task<Energy> GetEnergyThisMonthAsync(int month, CancellationToken cancellationToken = default);
		Task<Energy> GetEnergyThisWeekAsync(CancellationToken cancellationToken = default);
		Task<Energy> GetEnergyThisYearAsync(CancellationToken cancellationToken = default);
		Task<Energy> GetEnergyTodayAsync(int day, CancellationToken cancellationToken = default);
		Task<Overview> GetOverviewAsync(CancellationToken cancellationToken = default);
		Task<SiteDetailsDto> GetSiteDetailsAsync(CancellationToken cancellationToken = default);
		Task<Energy> GetEnergyWholeTimeAsync(CancellationToken cancellationToken = default);
	}
}
