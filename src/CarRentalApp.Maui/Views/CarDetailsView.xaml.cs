using CarRentalApp.ViewModels;
using CarRentalApp;

namespace CarRentalApp.Views;

public partial class CarDetailsView : ContentPage
{
    readonly MyApplication _evergineApplication;

    public CarDetailsView()
	{
		InitializeComponent();

        _evergineApplication = new MyApplication();
        EvergineView.Application = _evergineApplication;
		BindingContext = new CarDetailsViewModel();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}