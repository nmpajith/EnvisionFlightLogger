using EnvisionFlightLogger.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.Repositories
{
    public class AirCraftRepository : GenericRepository<AirCraftEntity>
    {
        public AirCraftRepository(DataContext context) : base(context)
        {
        }
    }
}
