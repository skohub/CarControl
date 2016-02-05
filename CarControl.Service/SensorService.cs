using CarConnect.Data.Infrastructure;
using CarConnect.Data.Repositories;
using CarConnect.Model;
using CarControl.Contract;

namespace CarControl.Service
{
    public class SensorService : ISensorService
    {
        private readonly IFloatSensorRepository _floatSensorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGpsLocationRepository _gpsLocationRepository;
        private readonly IGSensorRepository _gSensorRepository;

        public SensorService(IFloatSensorRepository floatSensorRepository, IUnitOfWork unitOfWork,
            IGpsLocationRepository gpsLocationRepository, IGSensorRepository gSensorRepository)
        {
            _floatSensorRepository = floatSensorRepository;
            _unitOfWork = unitOfWork;
            _gpsLocationRepository = gpsLocationRepository;
            _gSensorRepository = gSensorRepository;
        }

        #region ISensorService members

        public void RegisterValue(FloatSensorValue sensor)
        {
            _floatSensorRepository.Add(sensor);
            _unitOfWork.Commit();
        }

        public void RegisterLocation(GpsLocation location)
        {
            _gpsLocationRepository.Add(location);
            _unitOfWork.Commit();
        }

        public void RegisterGSensor(GSensor gsensor)
        {
            _gSensorRepository.Add(gsensor);
            _unitOfWork.Commit();
        }

        #endregion
    }
}
