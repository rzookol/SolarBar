using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarBar.Model.Dto
{
    public class SiteDetailsDto
    {
         public Details details { get; set; }
    }

    public class Details
    {
        public int id { get; set; }
        public string name { get; set; }
        public int accountId { get; set; }
        public string status { get; set; }
        public float peakPower { get; set; }
        public string lastUpdateTime { get; set; }
        public string currency { get; set; }
        public string installationDate { get; set; }
        public object ptoDate { get; set; }
        public string notes { get; set; }
        public string type { get; set; }
        public Location location { get; set; }
        public Primarymodule primaryModule { get; set; }
        public Uris uris { get; set; }
        public Publicsettings publicSettings { get; set; }


        public string ToolTipInfo()
        {
            return "Adres: " + "\n" +
                      "       " + location.address + "\n" +
                      "       " + location.city + "\n" +
                      "       " + location.country + "\n" +
                      "Pierwsze uruchomienie: " + "\n" +
                      "       " + installationDate + "\n" +
                      "Maksymalna produkcja:  " + "\n" +
                      "       " + peakPower + "MW\n";
        }
    }

    public class Location
    {
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string zip { get; set; }
        public string timeZone { get; set; }
        public string countryCode { get; set; }
    }

    public class Primarymodule
    {
        public string manufacturerName { get; set; }
        public string modelName { get; set; }
        public float maximumPower { get; set; }
    }

    public class Uris
    {
        public string SITE_IMAGE { get; set; }
        public string DATA_PERIOD { get; set; }
        public string DETAILS { get; set; }
        public string OVERVIEW { get; set; }
    }

    public class Publicsettings
    {
        public bool isPublic { get; set; }
    }
}
