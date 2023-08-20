using CarRentalApp.Models;

namespace CarRentalApp.Services
{
    public class CarService
    {
        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return new List<Manufacturer>
            {
                new Manufacturer
                {
                    Name = "Tesla",
                    Logo = "tesla_logo.png"
                },
                new Manufacturer
                {
                    Name = "Mazda",
                    Logo = "mazda_logo.png"
                },
                new Manufacturer
                {
                    Name = "BMW",
                    Logo = "bmw_logo.png"
                },
                new Manufacturer
                {
                    Name = "Ferrari",
                    Logo = "ferrari_logo.png"
                },
            };
        }

        public IEnumerable<Car> GetCars()
        {
            return new List<Car>
            {
                new Car
                {
                    Name = "Tesla Model X",
                    Image = "tesla_model_x.png",
                    Location = "Ranjingan, Wangon",
                    Seats = 4,
                    Price = 30.0d
                }, 
                new Car
                {
                    Name= "Tesla Model 3",
                    Image = "tesla_model_3.png",
                    Location = "Ranjingan, Wangon",
                    Seats = 4,
                    Price = 30.0d
                },
            };
        }
    }
}
