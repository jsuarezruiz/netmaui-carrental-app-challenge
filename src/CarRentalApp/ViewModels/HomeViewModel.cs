using CarRentalApp.Models;
using CarRentalApp.Services;
using CarRentalApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CarRentalApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        readonly CarService _carService;

        ObservableCollection<Manufacturer> _manufacturers;
        ObservableCollection<Car> _cars;

        public HomeViewModel(CarService carService)
        {
            _carService = carService;
        
            LoadData();
        }

        public ObservableCollection<Manufacturer> Manufacturers
        {
            get { return _manufacturers; }
            set
            {
                _manufacturers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        Car selectedCar;

        [RelayCommand]
        async Task GoToDetails(Car Car)
        {
            if (Car == null)
                return;

            await Shell.Current.GoToAsync(nameof(CarDetailsView), true, new Dictionary<string, object>
            {
                {"Car", Car }
            });
        }

        void LoadData()
        {
            Manufacturers = new ObservableCollection<Manufacturer>(_carService.GetManufacturers());
            Cars = new ObservableCollection<Car>(_carService.GetCars());
        }
    }
}