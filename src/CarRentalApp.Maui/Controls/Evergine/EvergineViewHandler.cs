namespace CarRentalApp.Controls
{
    public partial class EvergineViewHandler
    {
        public static IPropertyMapper<EvergineView, EvergineViewHandler> PropertyMapper = new PropertyMapper<EvergineView, EvergineViewHandler>(ViewMapper)
        {
            [nameof(EvergineView.Application)] = MapApplication,
        };

        public static CommandMapper<EvergineView, EvergineViewHandler> CommandMapper = new(ViewCommandMapper);

        public EvergineViewHandler()
            : base(PropertyMapper, CommandMapper)
        {
        }
    }
}
