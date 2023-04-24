using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.Entities
{
    public class AirCraftEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Make { get; set; } 
        public string Model { get; set; } 
        public string Registration { get; set; }
        public string Location { get; set; }
        public DateTime DateAndTime { get; set; }
        public byte[] Photo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
