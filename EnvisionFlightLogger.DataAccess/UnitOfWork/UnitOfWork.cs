using EnvisionFlightLogger.DataAccess.Entities;
using EnvisionFlightLogger.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        private AirCraftRepository _aircraftRepository;

        public AirCraftRepository AircraftRepository
        {
            get
            {
                if (_aircraftRepository == null)
                    _aircraftRepository = new AirCraftRepository(_context);

                return _aircraftRepository;
            }
        }

        public void SaveChanges()
        {
            _context.Commit();
        }
    }
}
