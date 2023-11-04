using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Core_Layer.Services
{
    public class TemperatureGetterService : ITemperatureGetterService
    {
        /* Private fields */
        private readonly ITemperature temperatureRepository;

        /* Constructor */
        public TemperatureGetterService(ITemperature temperatureRepository)
        {
            this.temperatureRepository = temperatureRepository;

        }

        /* Get all the temperature values */
        public Task<List<TemperatureSensor>> GetAllTemperature()
        {
            return temperatureRepository.GetAllTemperature();
        }

        /* Searching the temperature value by Date */
        public async Task<List<TemperatureSensor>> GetTemperatureByDate(string date)
        {
            return await temperatureRepository.GetTemperatureByDate(date);
        }

        /* Searching the temperature value by Time */
        public async Task<List<TemperatureSensor>> GetTemperatureByTime(string time)
        {
            return await temperatureRepository.GetTemperatureByTime(time);
        }
    }
}