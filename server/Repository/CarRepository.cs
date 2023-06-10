using server.Interfaces;
using server.Models;

namespace server.Repository
{
    public class CarRepository: ICarRepository
    {
        private DataContext ctx;
        public CarRepository(DataContext context)
        {
            ctx = context;
        }

        public ICollection<Car> GetCars(bool onlyActive = false, string? search = "")
        {
            var cars = ctx.Cars.Where(c => !onlyActive || c.Active);
            if(!string.IsNullOrEmpty(search))
            {
                cars = cars.Where(c => 
                    c.LicensePlateNumber.ToLower().Contains(search.ToLower()) || 
                    c.Manufacturer.ToLower().Contains(search.ToLower()) ||
                    c.Model.ToLower().Contains(search.ToLower()) ||
                    c.Color.ToLower().Contains(search.ToLower()) ||
                    c.Year.ToString().Contains(search.ToLower())
                    );
            }
            return cars.OrderBy(c => c.LicensePlateNumber).ToList();
        }

        public Car? GetCar(string licensePlateNumber)
        {
            return ctx.Cars.Where(c => c.LicensePlateNumber == licensePlateNumber).FirstOrDefault();
        }

        public void CreateCar(Car car)
        {
            ctx.Add(car);
            ctx.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            ctx.Update(car);
            ctx.SaveChanges();
        }

        public void ToggleCarActivation(Car car)
        {
            car.Active = !car.Active;
            ctx.SaveChanges();
        }

        public bool CarExists(string licensePlateNumber)
        {
            return ctx.Cars.Any(c => c.LicensePlateNumber == licensePlateNumber);
        }

    }
}
