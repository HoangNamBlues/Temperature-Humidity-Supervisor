namespace WebApi.Core_Layer.Service_Interfaces
{
    public interface IHumidityAdderService
    {
        Task<bool> AddHumidity(double humidity);
    }
}