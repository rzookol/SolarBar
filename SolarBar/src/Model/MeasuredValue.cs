using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarBar.Model
{
	public class MeasuredValue
	{
		public DateTime date;
		public float? value;
	}
}
