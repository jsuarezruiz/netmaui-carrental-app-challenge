using CarRentalApp.ViewModels;

namespace CarRentalApp.Views;

public partial class CarDetailsView : ContentPage
{
	public CarDetailsView(CarDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}