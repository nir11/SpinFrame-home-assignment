using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using server.Models;

namespace server.Controllers
{
    [ApiController]
    [Route("api/external")]
    [ApiExplorerSettings(GroupName = "external")]
    public class ExternalController : Controller
    {
        private readonly ICarRepository _carRepository;

        public ExternalController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [Route("cars")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        [HttpGet()]
        public ActionResult<ApiResponse<IEnumerable<Car>>> GetCars([FromQuery] bool onlyActive = false, [FromQuery] string? search = "")
        {
            try
            {
                var cars = _carRepository.GetCars(onlyActive, search);
                return new ApiResponse<IEnumerable<Car>>(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(ex));
            }
        }

        [Route("cars/{licensePlateNumber}")]
        [HttpGet()]
        [ProducesResponseType(404, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public ActionResult<ApiResponse<Car>> GetCar(string licensePlateNumber)
        {
            try
            {
                var car = _carRepository.GetCar(licensePlateNumber);
                if (car == null)
                    return new ApiResponse<Car>(Enums.Errors.CarNotFound);

                return new ApiResponse<Car>(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(ex));
            }
        }

    }
}
