using WebApi.Core_Layer.Domain.Entity_Classes;

namespace WebApi.Core_Layer.Domain.Repository_Interfaces
{
    public interface ITemperature
    {
        Task<List<TemperatureSensor>> GetAllTemperature();
        Task<List<TemperatureSensor>> GetTemperatureByDate(string date);
        Task<List<TemperatureSensor>> GetTemperatureByTime(string time);
        Task<bool> AddTemperature(double temperature);

        Task<List<TemperatureSensor>> DeleteTemperatureByDate(string date);

    }
}