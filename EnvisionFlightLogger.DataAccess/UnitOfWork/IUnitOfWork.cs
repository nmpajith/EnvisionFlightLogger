using EnvisionFlightLogger.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        AirCraftRepository AircraftRepository { get;}
        void SaveChanges();
    }
}
