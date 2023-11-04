using System.ComponentModel.DataAnnotations;

namespace WebApi.Core_Layer.Domain.Entity_Classes
{
    public class HumiditySensor
    {
        [Key]
        public Guid HumidityId { get; set; }
        public double HumidityValue { get; set; }
        public DateTime HumidityTime { get; set; }
    }
}