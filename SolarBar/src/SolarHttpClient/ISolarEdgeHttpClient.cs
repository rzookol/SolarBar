using SolarBar.Model;
using System.Threading.Tasks;
using System.Threading;
using System;
using SolarBar.Model.Dto;

namespace SolarBar.HttpClient
{
	public interface ISolarEdgeHttpClient
	{
		Task<OverviewDto> GetOverviewAsync(string siteId, CancellationToken cancellationToken);

		Task<EnergyDto> GetEnergyAsync(string siteId, DateTime start, 
			DateTime end, TimeUnit unit, CancellationToken cancellationToken =default);
        Task<SiteDetailsDto> GetSiteDetailsAsync(string siteId, CancellationToken cancellationToken);
    }
}