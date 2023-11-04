using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Core_Layer.Services
{
    public class TemperatureAdderService : ITemperatureAdderService
    {
        /* private fields */
        private readonly ITemperature temperatureRepository;

        // Constructor
        public TemperatureAdderService(ITemperature temperatureRepository)
        {
            this.temperatureRepository = temperatureRepository;

        }

        /* Add temperature */
        public async Task<bool> AddTemperature(double temperature)
        {
            if (await temperatureRepository.AddTemperature(temperature))
                return true;
            else
                return false;
        }
    }
}