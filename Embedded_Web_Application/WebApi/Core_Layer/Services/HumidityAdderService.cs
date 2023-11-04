using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Core_Layer.Services
{
    public class HumidityAdderService : IHumidityAdderService
    {
        /* private fields */
        private readonly IHumidity HumidityRepository;

        // Constructor
        public HumidityAdderService(IHumidity HumidityRepository)
        {
            this.HumidityRepository = HumidityRepository;

        }

        /* Add Humidity */
        public async Task<bool> AddHumidity(double Humidity)
        {
            if (await HumidityRepository.AddHumidity(Humidity))
                return true;
            else
                return false;
        }
    }
}