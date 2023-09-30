namespace CarRentalApp.Controls
{
    internal static class AppBuilderExtensions
    {
        public static MauiAppBuilder UseMauiEvergine(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(h =>
            {
                h.AddHandler<EvergineView, EvergineViewHandler>();
            });

            return builder;
        }
    }
}
