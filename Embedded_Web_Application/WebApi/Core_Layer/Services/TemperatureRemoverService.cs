using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Core_Layer.Services
{
    public class TemperatureRemoverService : ITemperatureRemoverService
    {
        /* Private fields */
        private readonly ITemperature temperatureRepository;

        /* Constructor */
        public TemperatureRemoverService(ITemperature temperatureRepository)
        {
            this.temperatureRepository = temperatureRepository;

        }

        /* Remove temperature */
        public Task<List<TemperatureSensor>> DeleteTemperatureByTime(string date)
        {
            return temperatureRepository.DeleteTemperatureByDate(date);
        }
    }
}