using EnvisionFlightLogger.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess
{
    public class DataContext : SQLite.SQLiteConnection
    {
        public DataContext(string dbPath) : base(dbPath)
        {
            CreateTable<AirCraftEntity>();
        }
    }
}
