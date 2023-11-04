using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.Identity;

namespace WebApi.Infrastructure_Layer.DbContext_Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }
        public DbSet<TemperatureSensor> TemperatureDbSets { set; get; }
        public DbSet<HumiditySensor> HumidityDbSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TemperatureSensor>().ToTable("Temperatures");
            modelBuilder.Entity<HumiditySensor>().ToTable("Humidities");
            TemperatureSensor temperatureSensor = new()
            {
                TemperatureId = Guid.NewGuid(),
                TemperatureValue = 26.55,
                TemperatureTime = DateTime.Now
            };
            HumiditySensor humiditySensor = new()
            {
                HumidityId = Guid.NewGuid(),
                HumidityValue = 93.25,
                HumidityTime = DateTime.Now
            };
            modelBuilder.Entity<TemperatureSensor>().HasData(temperatureSensor);
            modelBuilder.Entity<HumiditySensor>().HasData(humiditySensor);
        }

    }
}