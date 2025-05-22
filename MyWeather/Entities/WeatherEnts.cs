using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;


namespace MyWeather.Entities
{
    public class Readings
    {
        [Key]
        public int ReadingID { get; set; }
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Preasure { get; set; }
    }

    public class LogEvents
    {
        [Key]
        public int LogID { get; set; }
        public DateTime DateTime { get; set; }
        public string Event { get; set; }
    }
}
