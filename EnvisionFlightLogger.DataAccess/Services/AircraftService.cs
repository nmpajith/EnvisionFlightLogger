using EnvisionFlightLogger.DataAccess.Entities;
using EnvisionFlightLogger.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvisionFlightLogger.DataAccess.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AircraftService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<AirCraftEntity> GetAllAircrafts()
        {
            try
            {
                return _unitOfWork.AircraftRepository.GetAll();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AirCraftEntity GetAircraftById(int id)
        {
            return _unitOfWork.AircraftRepository.GetById(id);
        }

        public int AddAircraft(AirCraftEntity aircraftEntity)
        {
            try
            {
                _unitOfWork.AircraftRepository.Insert(aircraftEntity);
                _unitOfWork.SaveChanges();
                return aircraftEntity.Id;
            }
            catch(Exception)
            {
                return -1;
            }
        }

        public void UpdateAircraft(AirCraftEntity aircraftEntity)
        {
            _unitOfWork.AircraftRepository.Update(aircraftEntity);
            _unitOfWork.SaveChanges();
        }

        public void DeleteAircraft(int id)
        {
            _unitOfWork.AircraftRepository.Delete(id);
            _unitOfWork.SaveChanges();
        }
    }

}
