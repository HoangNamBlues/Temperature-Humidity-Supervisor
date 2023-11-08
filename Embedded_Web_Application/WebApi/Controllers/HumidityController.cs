using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core_Layer.Domain.Entity_Classes;
using WebApi.Core_Layer.DTOs;
using WebApi.Core_Layer.Service_Interfaces;

namespace WebApi.Controllers
{
    [Route("Api/[controller]")]
    public class HumidityController : Controller
    {
        /* Private fields */
        private readonly IHumidityRemoverService humidityRemoverService;
        private readonly IHumidityGetterService humidityGetterService;
        private readonly IHumidityAdderService humidityAdderService;

        /* Constructor */
        public HumidityController(IHumidityAdderService humidityAdderService, IHumidityGetterService humidityGetterService, IHumidityRemoverService humidityRemoverService)
        {
            this.humidityAdderService = humidityAdderService;
            this.humidityGetterService = humidityGetterService;
            this.humidityRemoverService = humidityRemoverService;
        }

        /* Get all the humidity values request */
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllHumidity()
        {
            List<HumiditySensor> humidityList = await humidityGetterService.GetAllHumidity();
            if (humidityList == null)
            {
                return NotFound();
            }
            else
            {
                List<HumidityResponse> humidityResponseList = new();
                foreach (var h in humidityList)
                {
                    humidityResponseList.Add(new HumidityResponse()
                    {
                        HumidityValue = h.HumidityValue,
                        HumidityTime = h.HumidityTime,
                        HumidityTimeString = h.HumidityTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(humidityResponseList);
            }
        }

        /* Search the humidity value by Date request */
        /* Date format: yyyy-dd-MM */
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        [Route("SearchByDate/{date}")]
        public async Task<IActionResult> GethumidityByDate([FromRoute] string date)
        {
            List<HumiditySensor> humidityList = await humidityGetterService.GetHumidityByDate(date);
            if (!humidityList.Any())
                return NotFound();
            else
            {
                List<HumidityResponse> humidityResponses = new();
                foreach (var h in humidityList)
                {
                    humidityResponses.Add(new HumidityResponse()
                    {
                        HumidityValue = h.HumidityValue,
                        HumidityTime = h.HumidityTime,
                        HumidityTimeString = h.HumidityTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(humidityResponses);
            }
        }

        /* Search the humidity value by Time request */
        /* Time format: HH:mm:ss */
        [HttpGet]
        [Authorize(Roles = "User,admin")]
        [Route("SearchByTime/{time}")]
        public async Task<IActionResult> GethumidityByTime([FromRoute] string time)
        {
            List<HumiditySensor> humidityList = await humidityGetterService.GetHumidityByTime(time);
            if (!humidityList.Any())
                return NotFound();
            else
            {
                List<HumidityResponse> humidityResponses = new();
                foreach (var h in humidityList)
                {
                    humidityResponses.Add(new HumidityResponse()
                    {
                        HumidityValue = h.HumidityValue,
                        HumidityTime = h.HumidityTime,
                        HumidityTimeString = h.HumidityTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(humidityResponses);
            }
        }

        /* Add the new humidity value request */
        [HttpPost]
        [Route("ESP32/AddHumidity/{value:double}")]
        public async Task<IActionResult> Addhumidity([FromRoute] double value)
        {
            var status = await humidityAdderService.AddHumidity(value);
            if (status.ToString() == "True")
                return Ok();
            else
                return BadRequest("Your request is invalid");
        }

        /* Delete the humidity value by date */
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("Delete/{date}")]
        public async Task<IActionResult> Deletehumidity([FromRoute] string date)
        {
            List<HumiditySensor> humidityList = await humidityRemoverService.DeleteHumidityByTime(date);
            if (!humidityList.Any())
                return NotFound();
            else
            {
                List<HumidityResponse> humidityResponse = new();
                foreach (var h in humidityList)
                {
                    humidityResponse.Add(new HumidityResponse()
                    {
                        HumidityValue = h.HumidityValue,
                        HumidityTime = h.HumidityTime,
                        HumidityTimeString = h.HumidityTime.ToString("HH:mm:ss || dddd, dd/MM/yyyy")
                    });
                }
                return Ok(humidityResponse);
            }
        }

    }
}