using server.Repository;

namespace server.Dto
{
    public class CarDto
    {
        public string LicensePlateNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public bool Active { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Year { get; set; }
    }
}
