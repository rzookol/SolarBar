using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SolarBar.Config;
using SolarBar.Model;
using SolarBar.Model.Dto;
using SolarBar.Extensions;

namespace SolarBar.HttpClient
{
	public class SolarEdgeHttpClient : ISolarEdgeHttpClient
	{
		private readonly BarConfig _config;
		private readonly IHttpClientFactory _clientFactory;

		public SolarEdgeHttpClient(BarConfig config, IHttpClientFactory clientFactory)
		{
			_config = config;
			_clientFactory = clientFactory;
		}

		public async Task<EnergyDto> GetEnergyAsync(string siteId, 
			DateTime start, DateTime end, 
			TimeUnit unit, CancellationToken cancellationToken)
		{
			return await GetSolarData<EnergyDto>($"site/{siteId}/energy", $"timeUnit={unit}&startDate={start.ConvertToURIStringDate()}&endDate={end.ConvertToURIStringDate()}", cancellationToken).ConfigureAwait(false);
		}

		public async Task<OverviewDto> GetOverviewAsync(string siteId, CancellationToken cancellationToken)
		{
			return await GetSolarData<OverviewDto>($"site/{siteId}/overview", "", cancellationToken).ConfigureAwait(false);
		}


		private static TSolarData Convert<TSolarData>(string content)
		{
			var jsonSettings = new JsonSerializerSettings
			{
				MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
				DateParseHandling = DateParseHandling.None,
				Error = (sender, args) =>
				{
					var currentObject = args.CurrentObject as MeasuredValue;
					if (currentObject != null && args.ErrorContext.Member.ToString() == "value")
					{
						currentObject.value = 0.0f; // Przypisz 0.0, gdy wystąpi błąd (np. wartość null)
						args.ErrorContext.Handled = true;
					}
				}
			};

			var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
			jsonSettings.Converters.Add(dateTimeConverter);

			return JsonConvert.DeserializeObject<TSolarData>(content, jsonSettings);
		}

		private async Task<TSolarData> GetSolarData<TSolarData>(string relativePath, string queryParams, CancellationToken cancellationToken)
		{
			var httpClient = _clientFactory.CreateClient("SolarBarHttpClient");

			var uri = new UriBuilder(new Uri(_config.Host));
				
			uri.Path += relativePath;

			var apiKey = $"api_key={_config.ApiKey}";

			if (string.IsNullOrEmpty(queryParams))
			{
				queryParams = apiKey;
			}
			else
			{
				queryParams += "&"+apiKey;
			}

			uri.Query += queryParams;
			try
			{
				var apiResult = await httpClient.GetAsync(uri.Uri, cancellationToken).ConfigureAwait(false);
				var strContent = await apiResult.Content.ReadAsStringAsync().ConfigureAwait(false);
				var result = cancellationToken.IsCancellationRequested ?
					default :
					Convert<TSolarData>(strContent);
				return result;
			}
			catch(Exception ex)
            {

            }
			return default(TSolarData);
		}

        public async Task<SiteDetailsDto> GetSiteDetailsAsync(string siteId, CancellationToken cancellationToken)
        {
			return await GetSolarData<SiteDetailsDto>($"site/{siteId}/details", "", cancellationToken).ConfigureAwait(false);
		}
    }
}
