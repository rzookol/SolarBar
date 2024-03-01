using SolarBar.Model;
using SolarBar.Model.Dto;


namespace SolarBar.Extensions
{
	public static class OverviewDtoExtensions
    {
		public static Overview ToOverview(this OverviewDto overviewDto)
		{
			var details = overviewDto.Overview;
			var result = new Overview
			{
				CurrentPower = details.CurrentPower.Power,
				LastDayData = details.LastDayData.Energy,
				LastMonthData = details.LastMonthData.Energy,
				LastYearData = details.LastYearData.Energy,
				LifeTimeData = details.LifeTimeData.Energy,
				Time = details.LastUpdateTime
			};

			return result;
		}
	}
}
