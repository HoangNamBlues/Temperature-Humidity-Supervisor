namespace WebApi.Core_Layer.DTOs
{
    public class TemperatureResponse
    {
        public double TemperatureValue { get; set; }
        public DateTime TemperatureTime { get; set; }
        public string TemperatureTimeString { get; set; }
    }
}