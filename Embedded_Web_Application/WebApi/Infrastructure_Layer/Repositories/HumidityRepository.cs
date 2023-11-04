using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.Domain.Repository_Interfaces;
using WebApi.Infrastructure_Layer.DbContext_Data;

namespace WebApi.Infrastructure_Layer.Repositories
{
    public class HumidityRepository : IHumidity
    {
        private readonly DataDbContext _dataDbContext;

        /* Constructor */
        public HumidityRepository(DataDbContext dataDbContext)

        {
            this._dataDbContext = dataDbContext;

        }

        /* Add Humidity method */
        public async Task<bool> AddHumidity(double Humidity)
        {
            if (Humidity.ToString() == null)
                return false;
            else
            {
                HumiditySensor HumiditySensor = new()
                {
                    HumidityId = Guid.NewGuid(),
                    HumidityValue = Humidity,
                    HumidityTime = DateTime.Now
                };
                await _dataDbContext.HumidityDbSets.AddAsync(HumiditySensor);
                await _dataDbContext.SaveChangesAsync();
                return true;
            }
        }

        /* Get all the Humidity values method*/
        public async Task<List<HumiditySensor>> GetAllHumidity()
        {
            return await _dataDbContext.HumidityDbSets.ToListAsync();
        }

        /* Search the Humidity values by date */
        public async Task<List<HumiditySensor>> GetHumidityByDate(string date)
        {
            List<HumiditySensor> HumiditySensors = await _dataDbContext.HumidityDbSets.Where(x =>
           x.HumidityTime.ToString().Contains(date)).ToListAsync();
            return HumiditySensors;
        }

        /* Search the Humidity values by time */
        public async Task<List<HumiditySensor>> GetHumidityByTime(string time)
        {
            List<HumiditySensor> HumiditySensors = await _dataDbContext.HumidityDbSets.Where(x =>
            x.HumidityTime.ToString().Contains(time)).ToListAsync();
            return HumiditySensors;
        }

        /* Delete the Humidity value by date */
        public async Task<List<HumiditySensor>> DeleteHumidityByDate(string date)
        {
            List<HumiditySensor> HumidityToBeDeleted = await _dataDbContext.HumidityDbSets.Where(x => x.HumidityTime.ToString().Contains(date)).ToListAsync();
            if (HumidityToBeDeleted.Any())
            {
                foreach (var t in HumidityToBeDeleted)
                {
                    _dataDbContext.HumidityDbSets.Remove(t);
                    _dataDbContext.SaveChanges();
                }
            }
            return HumidityToBeDeleted;
        }
    }
}