using System.ComponentModel.DataAnnotations;

namespace WebApi.Core_Layer.Domain.Entity_Classes
{
    public class TemperatureSensor
    {
        [Key]
        public Guid TemperatureId { get; set; }
        public double TemperatureValue { get; set; }
        public DateTime TemperatureTime { get; set; }
    }
}