using SolarBar.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace SolarBar.Model
{
	public class Overview
	{
		public int Id { get; set; }
		public DateTime Time { get; set; }
		public string Metric { get; set; }
		public double LifeTimeData { get; set; }
		public double LastYearData { get; set; }
		public double LastMonthData { get; set; }
		public double LastDayData { get; set; }
		public double CurrentPower { get; set; }

        public string ShowData(ShowMode mode)
        {
            switch (mode)
            {
                case ShowMode.Total:
                    return "Produkcja całkowita: " + LifeTimeData.ToStringWithPostfix();

                case ShowMode.Day:
                    return "Produkcja dzienna: " + LastDayData.ToStringWithPostfix();

                case ShowMode.Current:
                    return "Produkcja obecnie: " + CurrentPower.ToStringWithPostfix();

                case ShowMode.Month:
                    return "Produkcja miesięcznie: " + LastMonthData.ToStringWithPostfix();

                case ShowMode.Year:
                    return "Produkcja rocznie: " + LastYearData.ToStringWithPostfix();
            }

            return "";
        }
    }
}
