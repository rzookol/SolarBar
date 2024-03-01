using SolarBar.UI;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

namespace SolarBar.Model
{
	public class Energy
	{ 
		public string TimeUnit { get; set; }
		public string Unit { get; set; }
		public string MeasuredBy { get; set; }
		public MeasuredValue[] Values { get; set; }


        public override bool Equals(object obj)
        {
            var other = obj as Energy;
            if (other == null) return false;

            return TimeUnit == other.TimeUnit
                && Unit == other.Unit
                && MeasuredBy == other.MeasuredBy
                && Values.SequenceEqual(other.Values); 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }


        public void FilterValuesByHour(int? startHour, int? endHour)
        {
            if (startHour != null && endHour != null)
            {
                Values = Values.Where(v => v.date.Hour >= startHour && v.date.Hour <= endHour).ToArray();
            }
        }

        public void FilterValuesByNull(int? startHour, int? endHour)
        {
            int firstNonNull = Array.FindIndex(Values, v => v.value.HasValue && v.value.Value != 0);
            if (firstNonNull == -1) 
            {
                Values = Array.Empty<MeasuredValue>();
                return;
            }

            int lastNonNull = Array.FindLastIndex(Values, v => v.value.HasValue && v.value.Value != 0);

            Values = Values.Skip(firstNonNull).Take(lastNonNull - firstNonNull + 1).ToArray();
        }

        public float SumValues()
        {
            float sum = 0;
            foreach (var measuredValue in Values)
            {
                if (measuredValue.value.HasValue)
                {
                    sum += measuredValue.value.Value;
                }
            }
            return sum;
        }

        public string ShowTimeRange(ShowMode mode)
        {
            CultureInfo culture = new CultureInfo("pl-PL");

            if (Values.Length > 0)
            {
                switch (mode)
                {
                    case ShowMode.Total:
                        return $"{Values.First().date.Year} - {Values.Last().date.Year}";

                    case ShowMode.Day:
                        return Values.First().date.ToShortDateString();

                    case ShowMode.Current:
                        return DateTime.Now.ToString();

                    case ShowMode.Month:
                        return $"{Values.First().date.Day}.{Values.First().date.Month}.{Values.First().date.Year}  -  {Values.Last().date.Day}.{Values.Last().date.Month}.{Values.Last().date.Year}";

                    case ShowMode.Year:
                        return $"{Values.First().date.ToString("MMM", culture)} {Values.First().date.Year}  -  {Values.Last().date.ToString("MMM", culture)} {Values.Last().date.Year}";
                }
            }
            return DateTime.Now.ToShortDateString();
        }

    }

}
