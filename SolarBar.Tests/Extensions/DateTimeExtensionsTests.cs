using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SolarBar.Extensions;
using System.Globalization;

namespace SolarBar.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void FirstDayOfWeek_ShouldReturnCorrectDate()
        {
            var date = new DateTime(2022, 5, 4); // Środa
            var result = date.FirstDayOfWeek();

            // Assert
            Assert.AreEqual(new DateTime(2022, 5, 2), result); // Poniedziałek tego tygodnia
        }

        [Test]
        public void FirstDayOfMonth_ShouldReturnCorrectDate()
        {
            var date = new DateTime(2022, 5, 15);
            var result = date.FirstDayOfMonth();

            Assert.AreEqual(new DateTime(2022, 5, 1), result);
        }

        [Test]
        public void FirstDayOfYear_ShouldReturnCorrectDate()
        {
            var date = new DateTime(2022, 5, 15);


            var result = date.FirstDayOfYear();
            Assert.AreEqual(new DateTime(2022, 1, 1), result);
        }

        [Test]
        public void ConvertToURIStringTime_ShouldReturnCorrectFormat()
        {
            var date = new DateTime(2022, 5, 15, 13, 45, 30);


            var result = date.ConvertToURIStringTime();
            Assert.AreEqual("2022-05-15 13:45:30", result);
        }

        [Test]
        public void ConvertToURIStringDate_ShouldReturnCorrectFormat()
        {
            var date = new DateTime(2022, 5, 15);

            var result = date.ConvertToURIStringDate();
            Assert.AreEqual("2022-05-15", result);
        }
    }
}
