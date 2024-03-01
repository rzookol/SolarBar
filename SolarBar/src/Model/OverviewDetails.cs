using System;

namespace SolarBar.Model
{
	/// <summary>
	/// Site overview
	/// </summary>
	public class OverviewDetails
	{
		public DateTime LastUpdateTime { get; set; }

		public Data LifeTimeData { get; set; }

		public Data LastYearData { get; set; }

		public Data LastMonthData { get; set; }

		public Data LastDayData { get; set; }

		public CurrentPower CurrentPower { get; set; }

		public string MeasuredBy { get; set; }
	}
}
