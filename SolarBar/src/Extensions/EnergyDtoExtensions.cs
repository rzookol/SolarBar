using SolarBar.Model;
using SolarBar.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarBar.Extensions
{
    public static class EnergyDtoExtensions
    {
		public static Energy ToEnergy(this EnergyDto energy)
		{
		/*	var result = new Energy
			{
				MeasuredBy = energy.MeasuredBy,
				TimeUnit = energy.TimeUnit,
				Unit = energy.Unit,
				Values = energy.Values
			};*/

			return energy.energy;
		}
	}
}
