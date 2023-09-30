using CarRentalApp.ViewModels;
using CarRentalApp;

namespace CarRentalApp.Views;

public partial class CarDetailsView : ContentPage
{
    readonly MyApplication _evergineApplication;

    public CarDetailsView(CarDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

        _evergineApplication = new MyApplication();
        EvergineView.Application = _evergineApplication;
    }
}