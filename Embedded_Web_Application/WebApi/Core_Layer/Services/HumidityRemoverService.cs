using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Core_Layer.Services
{
    public class HumidityRemoverService : IHumidityRemoverService
    {
        /* Private fields */
        private readonly IHumidity HumidityRepository;

        /* Constructor */
        public HumidityRemoverService(IHumidity HumidityRepository)
        {
            this.HumidityRepository = HumidityRepository;

        }

        /* Remove Humidity */
        public Task<List<HumiditySensor>> DeleteHumidityByTime(string date)
        {
            return HumidityRepository.DeleteHumidityByDate(date);
        }
    }
}