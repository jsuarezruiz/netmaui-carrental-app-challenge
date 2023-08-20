using CarRentalApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRentalApp.ViewModels
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class CarDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        Car car;

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
