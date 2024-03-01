using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarBar.Model
{
    public enum ShowMode
    {
        Total,
        Year,
        Month,
        Day,
        Current
    }

    static class ShowModeExtensions
    {
        public static ShowMode NextMode(this ShowMode currentMode)
        {
            ShowMode[] allModes = (ShowMode[])Enum.GetValues(typeof(ShowMode));
            int index = (int)currentMode;
            return allModes[(index + 1) % allModes.Length];
        }
    }
}
