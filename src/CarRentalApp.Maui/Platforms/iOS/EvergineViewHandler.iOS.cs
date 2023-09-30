using CarRentalApp.MAUI.Platforms.iOS;
using Evergine.Common.Graphics;
using Evergine.Framework.Graphics;
using Evergine.Framework.Services;
using Evergine.IOSView;
using Microsoft.Maui.Handlers;
using System.Diagnostics;
using UIKit;

namespace CarRentalApp.Controls
{
    public partial class EvergineViewHandler : ViewHandler<EvergineView, UIView>
    {
        EvergineAppViewController evergineViewController;
        bool isViewLoaded;
        bool isEvergineInitialized = false;

        public static void MapApplication(EvergineViewHandler handler, EvergineView evergineView)
        {
            if (!handler.isViewLoaded)
            {
                return;
            }

            handler.UpdateApplication(evergineView);
        }

        protected override UIView CreatePlatformView()
        {
            this.evergineViewController = new EvergineAppViewController();
            this.ViewController = this.evergineViewController;

            return this.ViewController.View;
        }

        protected override void ConnectHandler(UIView platformView)
        {
            base.ConnectHandler(platformView);
            this.isViewLoaded = false;
            this.evergineViewController.OnViewDidLayoutSubviews += EvergineViewController_OnViewDidLayoutSubviews;
        }

        protected override void DisconnectHandler(UIView platformView)
        {
            base.DisconnectHandler(platformView);
            this.evergineViewController.OnViewDidLayoutSubviews -= EvergineViewController_OnViewDidLayoutSubviews;
        }

        void EvergineViewController_OnViewDidLayoutSubviews(object sender, EventArgs e)
        {
            this.isViewLoaded = true;
            this.UpdateValue(nameof(EvergineView.Application));
        }

        void UpdateApplication(EvergineView view)
        {
            if (view.Application is null || this.isEvergineInitialized)
            {
                return;
            }

            var application = view.Application;

            // Create services
            var windowsSystem = new IOSWindowsSystem(this.evergineViewController);
            application.Container.RegisterInstance(windowsSystem as WindowsSystem);
            var surface = windowsSystem.CreateSurface(0, 0);

            ConfigureGraphicsContext(application, surface);

            var clockTimer = Stopwatch.StartNew();
            windowsSystem.Run(
            () =>
            {
                application.Initialize();
            },
            () =>
            {
                var gameTime = clockTimer.Elapsed;
                clockTimer.Restart();

                application.UpdateFrame(gameTime);
                application.DrawFrame(gameTime);
            });

            this.evergineViewController.LoadAction?.Invoke();

            this.isEvergineInitialized = true;
        }

        static void ConfigureGraphicsContext(global::Evergine.Framework.Application application, Surface surface)
        {
            GraphicsContext graphicsContext = new global::Evergine.Metal.MTLGraphicsContext();
            graphicsContext.CreateDevice();
            SwapChainDescription swapChainDescription = new SwapChainDescription()
            {
                SurfaceInfo = surface.SurfaceInfo,
                Width = surface.Width,
                Height = surface.Height,
                ColorTargetFormat = PixelFormat.B8G8R8A8_UNorm,
                ColorTargetFlags = TextureFlags.RenderTarget | TextureFlags.ShaderResource,
                DepthStencilTargetFormat = PixelFormat.D32_Float,
                DepthStencilTargetFlags = TextureFlags.DepthStencil,
                SampleCount = TextureSampleCount.None,
                IsWindowed = true,
                RefreshRate = 60,
            };
            var swapChain = graphicsContext.CreateSwapChain(swapChainDescription);
            swapChain.VerticalSync = true;
            swapChain.FrameBuffer.IntermediateBufferAssociated = true;

            var graphicsPresenter = application.Container.Resolve<GraphicsPresenter>();
            var firstDisplay = new Display(surface, swapChain);
            graphicsPresenter.AddDisplay("DefaultDisplay", firstDisplay);

            application.Container.RegisterInstance(graphicsContext);
        }
    }
}