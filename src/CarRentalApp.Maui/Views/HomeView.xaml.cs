using CarRentalApp.ViewModels;

namespace CarRentalApp.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}