namespace WebApi.Core_Layer.Service_Interfaces
{
    public interface ITemperatureAdderService
    {
        Task<bool> AddTemperature(double temperature);
    }
}