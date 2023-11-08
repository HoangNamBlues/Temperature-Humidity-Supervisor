using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.DTOs;
using WebApi.Core_Layer.Service_Interfaces;
using WebApi.Infrastructure_Layer.DbContext_Data;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TemperatureController : Controller
    {
        /* Private fields */
        private readonly ITemperatureAdderService temperatureAdderService;
        private readonly ITemperatureGetterService temperatureGetterService;
        private readonly ITemperatureRemoverService temperatureRemoverService;

        /* Constructor */
        public TemperatureController(ITemperatureAdderService temperatureAdderService, ITemperatureGetterService temperatureGetterService, ITemperatureRemoverService temperatureRemoverService)
        {
            this.temperatureRemoverService = temperatureRemoverService;
            this.temperatureGetterService = temperatureGetterService;
            this.temperatureAdderService = temperatureAdderService;
        }

        /* Get all temperature values request */
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllTemperature()
        {
            List<TemperatureSensor> temperatureList = await temperatureGetterService.GetAllTemperature();
            if (temperatureList == null)
                return NotFound();
            else
            {
                List<TemperatureResponse> temperatureResponseList = new();
                foreach (var t in temperatureList)
                {
                    temperatureResponseList.Add(new TemperatureResponse()
                    {
                        TemperatureValue = t.TemperatureValue,
                        TemperatureTime = t.TemperatureTime,
                        TemperatureTimeString = t.TemperatureTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(temperatureResponseList);
            }
        }

        /* Search the temperature value by Date request */
        /* Date format: yyyy-MM-dd */
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        [Route("SearchByDate/{date}")]
        public async Task<IActionResult> GetTemperatureByDate([FromRoute] string date)
        {
            List<TemperatureSensor> temperatureList = await temperatureGetterService.GetTemperatureByDate(date);
            if (!temperatureList.Any())
                return NotFound();
            else
            {
                List<TemperatureResponse> temperatureResponses = new();
                foreach (var t in temperatureList)
                {
                    temperatureResponses.Add(new TemperatureResponse()
                    {
                        TemperatureValue = t.TemperatureValue,
                        TemperatureTime = t.TemperatureTime,
                        TemperatureTimeString = t.TemperatureTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(temperatureResponses);
            }
        }

        /* Search the temperature value by Time request */
        /* Time format: HH:mm:ss */
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        [Route("SearchByTime/{time}")]
        public async Task<IActionResult> GetTemperatureByTime([FromRoute] string time)
        {
            List<TemperatureSensor> temperatureList = await temperatureGetterService.GetTemperatureByDate(time);
            if (!temperatureList.Any())
                return NotFound();
            else
            {
                List<TemperatureResponse> temperatureResponses = new();
                foreach (var t in temperatureList)
                {
                    temperatureResponses.Add(new TemperatureResponse()
                    {
                        TemperatureValue = t.TemperatureValue,
                        TemperatureTime = t.TemperatureTime,
                        TemperatureTimeString = t.TemperatureTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(temperatureResponses);
            }
        }

        /* Add the new temperature value request */
        [HttpPost]
        [Route("ESP32/AddTemperature/{value:double}")]
        public async Task<IActionResult> AddTemperature([FromRoute] double value)
        {
            var status = await temperatureAdderService.AddTemperature(value);
            if (status.ToString() == "True")
                return Ok();
            else
                return BadRequest("Your request is invalid");
        }

        /* Delete the temperature value by date */
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("Delete/{date}")]
        public async Task<IActionResult> DeleteTemperature([FromRoute] string date)
        {
            List<TemperatureSensor> temperatureList = await temperatureRemoverService.DeleteTemperatureByTime(date);
            if (!temperatureList.Any())
                return NotFound();
            else
            {
                List<TemperatureResponse> temperatureResponse = new();
                foreach (var t in temperatureList)
                {
                    temperatureResponse.Add(new TemperatureResponse()
                    {
                        TemperatureValue = t.TemperatureValue,
                        TemperatureTime = t.TemperatureTime,
                        TemperatureTimeString = t.TemperatureTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(temperatureResponse);
            }
        }
    }
}