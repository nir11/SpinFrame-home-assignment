using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Car
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string LicensePlateNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public bool Active { get; set; }

        private static readonly string[] ValidColors = { "אדום", "ירוק", "כסוף" };
        private string _color = string.Empty;
        public string Color
        {
            get => _color;
            set
            {
                // check if the assigned color is one of the valid options
                if (ValidColors.Contains(value))
                    _color = value;
                else
                    throw new ArgumentException("Invalid color value");
            }
        }


        private static readonly string[] ValidManufacturers = { "Audi", "BMW", "Volkswagen" };
        private string _manufacturer = string.Empty;
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                // check if the assigned manufacturer is one of the valid options
                if (ValidManufacturers.Contains(value))
                    _manufacturer= value;
                else
                    throw new ArgumentException("Invalid manufacturer value");
            }
        }


        [Range(2010, 2021, ErrorMessage = "Year must be between 2010 and 2021")]
        public int Year { get; set; }
    }
}
