using WebApi.Core_Layer.Domain.Entity_Classes;

namespace WebApi.Core_Layer.Domain.Repository_Interfaces
{
    public interface IHumidity
    {
        Task<List<HumiditySensor>> GetAllHumidity();
        Task<List<HumiditySensor>> GetHumidityByDate(string date);
        Task<List<HumiditySensor>> GetHumidityByTime(string time);
        Task<bool> AddHumidity(double humidity);

        Task<List<HumiditySensor>> DeleteHumidityByDate(string date);
    }
}