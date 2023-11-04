using WebApi.Core_Layer.Domain.Entity_Classes;

namespace WebApi.Core_Layer.Service_Interfaces
{
    public interface ITemperatureGetterService
    {
        Task<List<TemperatureSensor>> GetAllTemperature();
        Task<List<TemperatureSensor>> GetTemperatureByDate(string date);
        Task<List<TemperatureSensor>> GetTemperatureByTime(string time);
    }
}