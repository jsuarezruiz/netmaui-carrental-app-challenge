using CarRentalApp.Views;

namespace CarRentalApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CarDetailsView), typeof(CarDetailsView));
        }
    }
}
