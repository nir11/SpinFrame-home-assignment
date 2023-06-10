using server.Models;

namespace server.Interfaces
{
    public interface ICarRepository
    {
        ICollection<Car> GetCars(bool onlyActive = false, string? search = "");
        Car? GetCar(string licensePlateNumber);
        void CreateCar(Car car);
        void UpdateCar(Car car);
        void ToggleCarActivation(Car car);
        bool CarExists(string licensePlateNumber);
    }
}
