using WebApi.Core_Layer.Domain.Entity_Classes;

namespace WebApi.Core_Layer.Service_Interfaces
{
    public interface IHumidityRemoverService
    {
        Task<List<HumiditySensor>> DeleteHumidityByTime(string date);
    }
}