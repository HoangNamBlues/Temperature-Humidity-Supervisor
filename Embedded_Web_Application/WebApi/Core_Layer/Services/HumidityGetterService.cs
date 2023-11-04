using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Core_Layer.Services
{
    public class HumidityGetterService : IHumidityGetterService
    {
        /* Private fields */
        private readonly IHumidity HumidityRepository;

        /* Constructor */
        public HumidityGetterService(IHumidity HumidityRepository)
        {
            this.HumidityRepository = HumidityRepository;

        }

        /* Get all the Humidity values */
        public Task<List<HumiditySensor>> GetAllHumidity()
        {
            return HumidityRepository.GetAllHumidity();
        }

        /* Searching the Humidity value by Date */
        public async Task<List<HumiditySensor>> GetHumidityByDate(string date)
        {
            return await HumidityRepository.GetHumidityByDate(date);
        }

        /* Searching the Humidity value by Time */
        public async Task<List<HumiditySensor>> GetHumidityByTime(string time)
        {
            return await HumidityRepository.GetHumidityByTime(time);
        }
    }
}