using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SolarBar.Config;
using SolarBar.HttpClient;
using SolarBar.Model;
using SolarBar.Model.Dto;
using SolarBar.Extensions;

namespace SolarBar.Services
{
	public class ProxyService : IProxyService
	{
		private readonly BarConfig _config;
		private readonly ISolarEdgeHttpClient _solarEdgeHttpClient;

		public ProxyService(BarConfig config, ISolarEdgeHttpClient solarEdgeHttpClient)
		{
			_config = config;
			_solarEdgeHttpClient = solarEdgeHttpClient;
		}

		public async Task<Energy> GetEnergyThisMonthAsync(int month, CancellationToken cancellationToken)
		{
			var startTime = DateTime.Today.FirstDayOfMonth();

            EnergyDto energyResult = await _solarEdgeHttpClient.GetEnergyAsync(
                            _config.SiteId,
                            startTime.AddMonths(month),
							(month == 0) ? DateTime.Now : startTime.AddMonths(month+1).AddMinutes(-1),
							TimeUnit.DAY,
                            cancellationToken
                        ).ConfigureAwait(false);

            return energyResult?.ToEnergy();
        }

		public async Task<Energy> GetEnergyThisWeekAsync(CancellationToken cancellationToken)
		{
			var startTime = DateTime.Today.FirstDayOfWeek();
			var energyResult = await _solarEdgeHttpClient.GetEnergyAsync(
				_config.SiteId,
				startTime,
				DateTime.Now,
				TimeUnit.HOUR,
				cancellationToken
			).ConfigureAwait(false);

			return energyResult?.ToEnergy();
		}

		public async Task<Energy> GetEnergyThisYearAsync(CancellationToken cancellationToken)
		{
			var startTime = DateTime.Today.FirstDayOfYear();

			var energyResult = await _solarEdgeHttpClient.GetEnergyAsync(
				_config.SiteId,
				startTime,
				DateTime.Now,
				TimeUnit.MONTH,
				cancellationToken
			).ConfigureAwait(false);

			return energyResult?.ToEnergy();
		}

		public async Task<Energy> GetEnergyTodayAsync(int day, CancellationToken cancellationToken = default)
		{
			var energy = await _solarEdgeHttpClient.GetEnergyAsync(
				_config.SiteId,
				DateTime.Today.Date.AddDays(day),
				(day == 0) ? DateTime.Now : DateTime.Today.Date.AddDays(day + 1).AddMinutes(-1),
				TimeUnit.QUARTER_OF_AN_HOUR,
				cancellationToken
			).ConfigureAwait(false);

			return energy?.ToEnergy();
		}

        public async Task<Energy> GetEnergyWholeTimeAsync(CancellationToken cancellationToken = default)
        {
			var startTime = DateTime.Today.FirstDayOfYear().AddYears(-4);

			var energyResult = await _solarEdgeHttpClient.GetEnergyAsync(
				_config.SiteId,
				startTime,
				DateTime.Now,
				TimeUnit.MONTH,
				cancellationToken
			).ConfigureAwait(false);

			return energyResult?.ToEnergy();
		
		}

        public async Task<Overview> GetOverviewAsync(CancellationToken cancellationToken = default)
		{
			var overviewResult = await _solarEdgeHttpClient.GetOverviewAsync(
				_config.SiteId,
				cancellationToken
			).ConfigureAwait(false);

			return overviewResult?.ToOverview();
		}

		public async Task<SiteDetailsDto> GetSiteDetailsAsync(CancellationToken cancellationToken = default)
		{
			var siteDetailsResult = await _solarEdgeHttpClient.GetSiteDetailsAsync(
				_config.SiteId,
				cancellationToken
			).ConfigureAwait(false);

			return siteDetailsResult;
		}
	}
}
