using WebApi.Core_Layer.Domain.Entity_Classes;

namespace WebApi.Core_Layer.Service_Interfaces
{
    public interface IHumidityGetterService
    {
        Task<List<HumiditySensor>> GetAllHumidity();
        Task<List<HumiditySensor>> GetHumidityByDate(string date);
        Task<List<HumiditySensor>> GetHumidityByTime(string time);
    }
}