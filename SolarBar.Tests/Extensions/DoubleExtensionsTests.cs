using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SolarBar.Extensions;

namespace SolarBar.Tests.Extensions
{
    [TestFixture]
    public class DoubleExtensionsTests
    {
        [Test]
        public void ToStringWithPostfix_LessThanThousand_ShouldReturnWh()
        {

            double number = 999;
            string result = number.ToStringWithPostfix();

            Assert.AreEqual("999,00 Wh", result);
        }

        [Test]
        public void ToStringWithPostfix_Thousands_ShouldReturnKWh()
        {
            double number = 1000;
            string result = number.ToStringWithPostfix();

            Assert.AreEqual("1,00 kWh", result);
        }

        [Test]
        public void ToStringWithPostfix_Millions_ShouldReturnMWh()
        {
            double number = 1e6;
            string result = number.ToStringWithPostfix();

            Assert.AreEqual("1,00 MWh", result);
        }

        [Test]
        public void ToStringWithPostfix_Billions_ShouldReturnGWh()
        {
            double number = 1e9;
            string result = number.ToStringWithPostfix();

            Assert.AreEqual("1,00 GWh", result);
        }

        [Test]
        public void ToStringWithPostfix_NegativeValue_ShouldAlsoWork()
        {
            double number = -500;
            string result = number.ToStringWithPostfix();

            Assert.AreEqual("-500,00 Wh", result);
        }
    }
}
