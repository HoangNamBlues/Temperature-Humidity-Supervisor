using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Core_Layer.DTOs
{
    public class HumidityResponse
    {
        public double HumidityValue { get; set; }
        public DateTime HumidityTime { get; set; }
        public string HumidityTimeString { get; set; }
    }
}