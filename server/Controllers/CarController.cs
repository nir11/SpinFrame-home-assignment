using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Interfaces;
using server.Models;
using System.Data;

namespace server.Controllers
{
    //[Authorize] - This route would be protected in real world
    [ApiController]
    [Route("api/cars")]
    [ApiExplorerSettings(GroupName = "api")]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }


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

        [HttpPost]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public ActionResult<ApiResponse> CreateCar([FromBody] CarDto newCar)
        {
            try
            {
                if (_carRepository.CarExists(newCar.LicensePlateNumber))
                    return new ApiResponse(Enums.Errors.CarAlreadyExists);

                _carRepository.CreateCar(new Car
                {
                    Active = true,
                    Color = newCar.Color,
                    Model = newCar.Model,
                    LicensePlateNumber = newCar.LicensePlateNumber,
                    Manufacturer = newCar.Manufacturer,
                    Year = newCar.Year,
                });

                return Ok(new ApiResponse());
            }
            catch (Exception ex)
            { 
                return StatusCode(500, new ApiResponse(ex));
            }

        }

        [HttpPut("{licensePlateNumber}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<ApiResponse> UpdateCar(string licensePlateNumber, [FromBody] CarDto updatedCar)
        {
            try
            {
                if (updatedCar == null)
                    return BadRequest(new ApiResponse());

                var caroToUpdate = _carRepository.GetCar(licensePlateNumber);
                if (caroToUpdate == null)
                    return new ApiResponse(Enums.Errors.CarNotFound);

                if (caroToUpdate.LicensePlateNumber != updatedCar.LicensePlateNumber && _carRepository.CarExists(updatedCar.LicensePlateNumber))
                    return new ApiResponse(Enums.Errors.CarAlreadyExists);

                caroToUpdate.Year = updatedCar.Year;
                caroToUpdate.LicensePlateNumber = updatedCar.LicensePlateNumber;
                caroToUpdate.Color = updatedCar.Color;
                caroToUpdate.Manufacturer = updatedCar.Manufacturer;
                caroToUpdate.Model = updatedCar.Model;

                _carRepository.UpdateCar(caroToUpdate);

                return Ok(new ApiResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(ex));
            }

        }

        [HttpPut("active/{licensePlateNumber}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<ApiResponse> ToggleCarActivation(string licensePlateNumber)
        {
            try
            {
                var caroToToggle = _carRepository.GetCar(licensePlateNumber);
                if (caroToToggle == null)
                    return new ApiResponse(Enums.Errors.CarNotFound);

                _carRepository.ToggleCarActivation(caroToToggle);

                return Ok(new ApiResponse());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(ex));
            }
        }

    }
}