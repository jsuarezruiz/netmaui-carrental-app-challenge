using Evergine.IOSView;
using UIKit;

namespace CarRentalApp.MAUI.Platforms.iOS
{
    public partial class EvergineAppViewController : EvergineViewController
    {
        public event EventHandler OnViewDidLayoutSubviews;

        public EvergineAppViewController(ObjCRuntime.NativeHandle handle) : base(handle)
        {
        }

        public EvergineAppViewController() : base()
        {
        }

        public override void LoadView()
        {
            base.LoadView();
            this.View = new UIView();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            this.OnViewDidLayoutSubviews?.Invoke(this, EventArgs.Empty);
        }
    }
}
