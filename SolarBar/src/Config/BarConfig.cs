namespace SolarBar.Config
{
	public class BarConfig
	{
		public string SiteId { get; set; }
		public string ApiKey { get; set; }
		public string Host { get; set; }
		public int? ChartStartHour { get; set; }
		public int? ChartEndHour { get; set; }
		public string ConfigPath { get; set; }
	}
}
