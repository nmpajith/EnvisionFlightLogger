using EnvisionFlightLogger.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.Services
{
    public interface IAircraftService
    {
        IQueryable<AirCraftEntity> GetAllAircrafts();
        AirCraftEntity GetAircraftById(int id);
        int AddAircraft(AirCraftEntity aircraftEntity);
        void UpdateAircraft(AirCraftEntity aircraftEntity);
        void DeleteAircraft(int id);
    }
}
