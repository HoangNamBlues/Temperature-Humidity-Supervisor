using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Infrastructure_Layer.DbContext_Data;

namespace WebApi.Infrastructure_Layer.Repositories
{
    public class TemperatureRepository : ITemperature
    {
        /* private fields */
        private readonly DataDbContext _dataDbContext;

        /* Constructor */
        public TemperatureRepository(DataDbContext dataDbContext)

        {
            this._dataDbContext = dataDbContext;

        }

        /* Add temperature method */
        public async Task<bool> AddTemperature(double temperature)
        {
            if (temperature.ToString() == null)
                return false;
            else
            {
                TemperatureSensor temperatureSensor = new()
                {
                    TemperatureId = Guid.NewGuid(),
                    TemperatureValue = temperature,
                    TemperatureTime = DateTime.Now
                };
                await _dataDbContext.TemperatureDbSets.AddAsync(temperatureSensor);
                await _dataDbContext.SaveChangesAsync();
                return true;
            }
        }

        /* Get all the temperature values method*/
        public async Task<List<TemperatureSensor>> GetAllTemperature()
        {
            return await _dataDbContext.TemperatureDbSets.ToListAsync();
        }

        /* Search the temperature values by Date */
        public async Task<List<TemperatureSensor>> GetTemperatureByDate(string date)
        {
            List<TemperatureSensor> temperatureSensors = await _dataDbContext.TemperatureDbSets.Where(x =>
           x.TemperatureTime.ToString().Contains(date)).ToListAsync();
            return temperatureSensors;
        }

        /* Search the temperature values by Time */
        public async Task<List<TemperatureSensor>> GetTemperatureByTime(string time)
        {
            List<TemperatureSensor> temperatureSensors = await _dataDbContext.TemperatureDbSets.Where(x =>
            x.TemperatureTime.ToString().Contains(time)).ToListAsync();
            return temperatureSensors;
        }

        /* Delete the temperature value by Date */
        public async Task<List<TemperatureSensor>> DeleteTemperatureByDate(string date)
        {
            List<TemperatureSensor> temperatureToBeDeleted = await _dataDbContext.TemperatureDbSets.Where(x => x.TemperatureTime.ToString().Contains(date)).ToListAsync();
            if (temperatureToBeDeleted.Any())
            {
                foreach (var t in temperatureToBeDeleted)
                {
                    _dataDbContext.TemperatureDbSets.Remove(t);
                    _dataDbContext.SaveChanges();
                }
            }
            return temperatureToBeDeleted;
        }
    }
}