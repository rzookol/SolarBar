using System;

namespace SolarBar.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToStringWithPostfix(this double number)
        {
            if (Math.Abs(number) < 1e3)
            {
                return $"{number:F2} Wh";
            }
            else if (Math.Abs(number) < 1e6)
            {
                return $"{number / 1e3:F2} kWh";
            }
            else if (Math.Abs(number) < 1e9)
            {
                return $"{number / 1e6:F2} MWh";
            }
            else
            {
                return $"{number / 1e9:F2} GWh";
            }
        }
    }
}
