using WebApi.Core_Layer.Domain.Entity_Classes;

namespace WebApi.Core_Layer.Service_Interfaces
{
    public interface ITemperatureRemoverService
    {
        Task<List<TemperatureSensor>> DeleteTemperatureByTime(string date);
    }
}